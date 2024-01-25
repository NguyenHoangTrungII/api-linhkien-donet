using linhkien_donet.Entities;
using linhkien_donet.Interfaces;
using linhkien_donet.Models.CartModels;
using linhkien_donet.Models.UserModels;
using linhkien_donet.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace linhkien_donet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserRepository userRepository,IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPatch("Avatar")]
        [Authorize(Roles = ("USER, ADMIN"))]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateAvatar([FromForm] UpdateAvatarModel request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var updateAvatar = new UpdateAvatarRequest()
            {
                UserId = userId,
                Avatar = request.Avatar,
            };
           

            var result = await _userRepository.UpdateAvatar(updateAvatar);

            if (result.isSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
