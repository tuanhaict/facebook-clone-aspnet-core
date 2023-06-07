using Fadebook.Application.Models.AuthModel;
using Fadebook.Application.Models.TokenModel;
using Fadebook.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fadebook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthController(IServiceManager service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult> Signup([FromBody] SignupRequestDto signupRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var result = await _service.AuthService.Signup(signupRequestDto);
            return Ok(result);
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var loginResponseDto = await _service.AuthService.Login(loginRequestDto);
            return Ok(loginResponseDto);
        }
        [HttpPost]
        [Route("logout")]
        [Authorize]
        public ActionResult Logout()
        {
            return Ok("Log out successfully!!");
        }
        [HttpPost]
        [Route("refresh-token")]
        public ActionResult<Token> RefreshToken([FromBody] string refreshToken)
        {
            return Ok(_service.AuthService.RefreshToken(refreshToken));
        }
    }
}
