using _01_WEBAPI.Helper.ApiResults;
using _01_WEBAPI.Models;
using linhkien_donet.Entities;
using linhkien_donet.Helper.AuthResults;
using linhkien_donet.Interfaces;
using linhkien_donet.Models.AuthModels;
using linhkien_donet.Models.EmailModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace linhkien_donet.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;


        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<ApiResult<bool>> SeedRolesAsync()
        {
            bool isOwnerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.OWNER);
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.USER);

            if (isOwnerRoleExists && isAdminRoleExists && isUserRoleExists)
                return new ApiFailResult<bool>("Role exist");
               

            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.USER));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.ADMIN));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.OWNER));

            return new ApiSuccessResult<bool>("Role Seeding Done Successfully");
           
        }

        public async Task<ApiResult<bool>> RegisterAsync(RegisterModel registerDto)
        {
            var isExistsUser = await _userManager.FindByNameAsync(registerDto.UserName);

            if (isExistsUser != null)
                return new ApiFailResult<bool>("UserName Already Exists");

            if (!IsValidEmail(registerDto.Email))
            {
                return new ApiFailResult<bool>("Email Format Wrong");
            }

            var isExistsEmail = await _userManager.FindByNameAsync(registerDto.Email);

            if (isExistsEmail != null)
                return new ApiFailResult<bool>("Email Already Exists");

            ApplicationUser newUser = new ApplicationUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!createUserResult.Succeeded)
            {
                var errorString = "User Creation Failed Beacause: ";
                foreach (var error in createUserResult.Errors)
                {
                    errorString += " # " + error.Description;
                }
                return new ApiFailResult<bool>(errorString);
            }

            // Add a Default USER Role to all users
            await _userManager.AddToRoleAsync(newUser, StaticUserRoles.USER);

            return new ApiSuccessResult<bool>("User Created Successfully");
             
        }
        public async Task<ApiResult<LoginResponse>> LoginAsync(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);

            if (user is null)
                return new ApiFailResult<LoginResponse>("Invalid Credentials");
                

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginModel.Password);

            if (!isPasswordCorrect)
                return new ApiFailResult<LoginResponse>("Invalid Credentials");
                 

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("JWTID", Guid.NewGuid().ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GenerateNewJsonWebToken(authClaims);

            var response = new LoginResponse()
            {
                Id = user.Id,
                Token = token,
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar
            };

            return new ApiSuccessResult<LoginResponse>(response);
            
        }

        public async Task<ApiResult<bool>> MakeAdminAsync(UpdatePermission updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
                return new ApiFailResult<bool>("Invalid User name");
               

            await _userManager.AddToRoleAsync(user, StaticUserRoles.ADMIN);

            return new ApiSuccessResult<bool>("User is now an ADMIN");
                
             
        }

        public async Task<ApiResult<bool>> MakeOwnerAsync(UpdatePermission updatePermissionDto)
        {
            var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);

            if (user is null)
                return new ApiFailResult<bool>("Invalid User name");
            

            await _userManager.AddToRoleAsync(user, StaticUserRoles.OWNER);

            return new ApiSuccessResult<bool>("User is now an OWNER");
        }

        public async Task<ApiResult<bool>> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            

            if (user == null)
            {
                return new ApiFailResult<bool>("Email is not exists");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = $"https://website.com/resetpassword?userId={user.Id}&token={HttpUtility.UrlEncode(token)}";
            var emailContent = new EmailContent
            {
                To = email,
                Subject = "Resert Password",
                Body = $"Please  access to  {HttpUtility.UrlEncode(token)} & {user.Id} to reset your password."
            };

            var result  = await _emailService.SendMail(emailContent);

            if (!result.isSuccess)
            {
                return new ApiFailResult<bool>(result.Message);

            }

            return new ApiSuccessResult<bool>(result.Message);
        }

        //public async Task<ApiResult<bool>> ResetPassword(string userId, string token, string newPassword)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null || !await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token))
        //    {
        //        return new ApiFailResult<bool>("Invalid user or token.");
        //    }

        //    // Kiểm tra hết hạn của token
        //    var tokenValidUntil = await _userManager.GetTokenExpirationAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword");

        //    if (DateTime.Now > tokenValidUntil)
        //    {
        //        return new ApiFailResult<bool>("Token has expired. Please request a new password reset.");
        //    }

        //    // Thiết lập mật khẩu mới
        //    var resetResult = await _userManager.ResetPasswordAsync(user, token, newPassword);

        //    if (resetResult.Succeeded)
        //    {
        //        // Gửi thông báo hoặc thực hiện các bước khác sau khi đặt lại mật khẩu thành công.
        //        return new ApiSuccessResult<bool>("Password reset successful.");
        //    }
        //    else
        //    {
        //        // Xử lý lỗi khi đặt lại mật khẩu không thành công.
        //        return new ApiFailResult<bool>("Failed to reset password.");
        //    }
        //}


        public async Task<ApiResult<bool>> ResetPassword(ResetPasswordRequest request)
        {
            var decodedToken = HttpUtility.UrlDecode(request.token);

            var user = await _userManager.FindByIdAsync(request.userId ); // Thay thế bằng mã người dùng tương ứng

            if (user == null)
            {
                return new ApiFailResult<bool>("User is not exists");
            }

            var isValidToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", decodedToken);

            if (!isValidToken)
            {
                return new ApiFailResult<bool>("Something wrong ! pls try another time");

            }

            var resetResult = await _userManager.ResetPasswordAsync(user, decodedToken, request.newPassword);


            if (resetResult.Succeeded)
            {
                return new ApiSuccessResult<bool>("Password reset successful.");
            }
            else
            {
                return new ApiFailResult<bool>("Failed to reset password.");
            }

        }


        private string GetUserIdFromToken(string token)
        {
            var tokenService = new TokenService(_configuration);
            return tokenService.GetUserIdFromToken(token);
        }


        private string GenerateNewJsonWebToken(List<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenObject = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }

        

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                return Regex.IsMatch(email,
                    @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
                    RegexOptions.IgnorePatternWhitespace);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
