using Fadebook.Application.Extensions;
using Fadebook.Application.Models.CommentModel;
using Fadebook.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fadebook.WebAPI.Controllers
{
    [Route("api/posts/{postId:guid}/comments")]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly IServiceManager _services;
        private readonly IConfiguration _configuration;
        public CommentController(IServiceManager service, IConfiguration configuration)
        {
            _services = service;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CommentReturnDto>>> GetCommentsOfPost(Guid postId)
        {
            var comments = await _services.CommentService.GetCommentsOfPost(postId);
            return Ok(comments);
        }
        [HttpPost]
        [ActionName("CreateComment")]
        public async Task<ActionResult<CommentReturnDto>> CreateComment([FromBody] CommentWriteDto comment, Guid postId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = AuthExtension.GetUserIdFromTokenFromContext(HttpContext, _configuration);
            var commentReturn = await _services.CommentService.CreateComment(comment, userId, postId);
            return CreatedAtAction("CreateComment", commentReturn);
        }
        [HttpPut]
        [Route("{commentId:guid}")]
        public async Task<IActionResult> UpdateComment([FromBody] CommentWriteDto comment, Guid commentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = AuthExtension.GetUserIdFromTokenFromContext(HttpContext, _configuration);
            await _services.CommentService.UpdateComment(comment, commentId, userId);
            return NoContent();
        }
        [HttpDelete]
        [Route("{commentId:guid}")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            var userId = AuthExtension.GetUserIdFromTokenFromContext(HttpContext, _configuration);
            await _services.CommentService.DeleteComment(commentId, userId);
            return NoContent();
        }

    }
}
