using _01_WEBAPI.Data;
using _01_WEBAPI.Helper.ApiResults;
using _01_WEBAPI.Models;
using linhkien_donet.Entities;
using linhkien_donet.Interfaces;
using linhkien_donet.Models.UserModels;
using linhkien_donet.Services;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using static System.Net.Mime.MediaTypeNames;

namespace linhkien_donet.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        //private readonly IFileStorageService _fileStorageService;
        private readonly ICloudinaryService _cloudinaryService;

        public UserRepository(DataContext context, IFileStorageService fileStorageService, ICloudinaryService cloudinaryService)
        {
            _context = context;
            //_fileStorageService = fileStorageService;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<ApiResult<bool>> UpdateAvatar( UpdateAvatarRequest request)
        {
            try
            {
                var user = await _context.User.FirstOrDefaultAsync(u => u.Id == request.UserId); 
                if (user == null)
                {
                    return new ApiFailResult<bool>("Need Login");
                }
                var images = _cloudinaryService.UploadPhoto(request.Avatar);

                user.Avatar = images;


                _context.User.Update(user);
                var result = await _context.SaveChangesAsync() > 0;
                return new ApiSuccessResult<bool>(images);
                        }
            catch (Exception ex)
            {
                return new ApiFailResult<bool>(ex.Message);
            }
        }

    }
}
