using _01_WEBAPI.Models;
using linhkien_donet.Entities;
using linhkien_donet.Interfaces;
using linhkien_donet.Models.AuthModels;
using linhkien_donet.Models.EmailModels;
using linhkien_donet.Validators.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace linhkien_donet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController: Controller
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager )
        {
            _authService = authService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("seed-roles")]
        [Authorize(Roles = "ADMIN, OWNER")]
        public async Task<IActionResult> SeedRoles()
        {
            var seerRoles = await _authService.SeedRolesAsync();

            return Ok(seerRoles);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerDto)
        {
            var validator = new RegisterModelValidator();

            var validationResult = validator.Validate(registerDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var registerResult = await _authService.RegisterAsync(registerDto);

            if (registerResult.isSuccess)
                return Ok(registerResult);

            return BadRequest(registerResult);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel loginDto)
        {


            var validator = new LoginModelValidator();

            var validationResult = validator.Validate(loginDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var loginResult = await _authService.LoginAsync(loginDto);

            if (loginResult.isSuccess)
                return Ok(loginResult);

            return Unauthorized(loginResult);
        }

       
        [HttpPost]
        [Route("make-admin")]
        [Authorize(Roles = "ADMIN, OWNER")]
        public async Task<IActionResult> MakeAdmin([FromBody] UpdatePermission updatePermissionDto)
        {
            var operationResult = await _authService.MakeAdminAsync(updatePermissionDto);

            if (operationResult.isSuccess)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

        [HttpPost]
        [Route("make-owner")]
        [Authorize(Roles = "ADMIN, OWNER")]
        public async Task<IActionResult> MakeOwner([FromBody] UpdatePermission updatePermissionDto)
        {
            var operationResult = await _authService.MakeOwnerAsync(updatePermissionDto);

            if (operationResult.isSuccess)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

        [HttpPost]
        [Route("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound(new { Message = "Email not found" });
            }

            var operationResult = await _authService.ForgotPassword(email);

            if (operationResult.isSuccess)
                return Ok(operationResult);

            return BadRequest(operationResult);

        }

        [HttpPost]
        [Route("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _authService.ResetPassword(request);

            if (result.isSuccess)
                return Ok(result);

            return Unauthorized(result);
        }
    }
    

}
