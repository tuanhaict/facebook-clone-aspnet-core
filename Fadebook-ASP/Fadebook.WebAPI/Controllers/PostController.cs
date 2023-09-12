using Fadebook.Application.Extensions;
using Fadebook.Application.Models.PostModel;
using Fadebook.Application.Services.Interfaces;
using Fadebook.Domain.Exceptions;
using Fadebook.Infracstructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fadebook.WebAPI.Controllers
{
    [ApiController]
    [Route("api/posts")]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IServiceManager _services;
        private readonly IConfiguration _configuration;
        public PostController(IServiceManager service, IConfiguration configuration)
        {
            _services = service;
            _configuration = configuration;
        }
        [HttpGet]
        [Route("{id:guid}", Name ="GetPost")]
        public async Task<ActionResult<PostReturnDto>> GetPost(Guid id)
        {
            var post = await _services.PostService.GetPost(id);
            return Ok(post);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PostReturnDto>>> GetPosts()
        {
            var posts = await _services.PostService.GetPosts();
            return Ok(posts);
        }
        [HttpPost]
        public async Task<ActionResult<PostReturnDto>> CreatePost([FromForm] PostCreateDto post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = AuthExtension.GetUserIdFromTokenFromContext(HttpContext, _configuration);
            var postReturn = await _services.PostService.CreatePost(post, userId);
            return CreatedAtRoute("GetPost", new { id = postReturn.Id}, postReturn);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatePost([FromForm] PostUpdateDto post, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 
            var userId = AuthExtension.GetUserIdFromTokenFromContext(HttpContext, _configuration);
            await _services.PostService.UpdatePost(post, id, userId);
            return NoContent();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var userId = AuthExtension.GetUserIdFromTokenFromContext(HttpContext, _configuration);
            await _services.PostService.DeletePost(id, userId);
            return NoContent();
        }
        [HttpPost]
        [Route("{postId:guid}/reactions")]
        public async Task<IActionResult> ToogleLikePost(Guid postId)
        {
            var userId = AuthExtension.GetUserIdFromTokenFromContext(HttpContext, _configuration);
            await _services.PostService.ToogleLikePost(userId, postId);
            return NoContent();
        }
    }
}
