using Fadebook.Application.Extensions;
using Fadebook.Application.Models.IntroductionModel;
using Fadebook.Application.Models.UserModel;
using Fadebook.Application.Services.Interfaces;
using Fadebook.Domain.Exceptions;
using Fadebook.Infracstructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fadebook.WebAPI.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _services;
        private readonly IConfiguration _configuration;
        public UserController(IServiceManager services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<UserResponseDto>> GetUserById(Guid id)
        {
            var user = await _services.UserService.GetUserById(id);
            return Ok(user);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
                
            }
            var userId = AuthExtension.GetUserIdFromTokenFromContext(HttpContext, _configuration);
            await _services.UserService.UpdateUser(user, userId);

            return NoContent();
        }
        [HttpPost]
        [Route("upload-avatar")]
        public async Task<ActionResult<string>> UploadAvatar([FromForm] IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = AuthExtension.GetUserIdFromTokenFromContext(HttpContext, _configuration);
            var avatarUrl = await _services.UserService.UploadAvatar(file, userId);
            return Ok(avatarUrl);
        }
        [HttpPost]
        [Route("introductions")]
        [ActionName("AddUserIntroduction")]
        public async Task<ActionResult<IntroductionReadDto>> AddUserIntroduction([FromBody] IntroductionWriteDto introduction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = AuthExtension.GetUserIdFromTokenFromContext(HttpContext, _configuration);
            var introductionReturn = await _services.UserService.AddUserIntroduction(introduction, userId);
            return CreatedAtAction("AddUserIntroduction", introductionReturn);
        }
        [HttpGet]
        [Route("friends/get-friends-requests")]
        public async Task<ActionResult<IReadOnlyList<UserForDisplayDto>>> GetFriendsRequests()
        {
            var userId = AuthExtension.GetUserIdFromTokenFromContext(HttpContext, _configuration);
            var friends = await _services.UserService.GetFriendsRequests(userId);
            return Ok(friends);
        }
        [HttpGet]
        [Route("friends")]
        public async Task<ActionResult<IReadOnlyList<UserForDisplayDto>>> GetFriends([FromQuery] Guid? userId)
        {
            var targetUserId = userId ?? AuthExtension.GetUserIdFromTokenFromContext(HttpContext, _configuration);
            var friends = await _services.UserService.GetFriends(targetUserId);
            return Ok(friends);
        }
        [HttpPost]
        [Route("friends/{friendId:guid}")]
        public async Task<IActionResult> AddFriend(Guid friendId)
        {
            var userId = AuthExtension.GetUserIdFromTokenFromContext(HttpContext, _configuration);
            await _services.UserService.AddFriend(userId, friendId);
            return NoContent();
        }
        [HttpPut]
        [Route("friends/{friendId:guid}")]
        public async Task<IActionResult> AcceptOrUnFriend(Guid friendId)
        {
            var userId = AuthExtension.GetUserIdFromTokenFromContext(HttpContext, _configuration);
            await _services.UserService.AcceptOrUnFriend(userId, friendId);
            return NoContent();
        }
    }
}
