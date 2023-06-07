using Fadebook.Application.Models.CommentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Services.Interfaces
{
    public interface ICommentService
    {
        public Task<IReadOnlyList<CommentReturnDto>> GetCommentsOfPost(Guid postId);
        public Task UpdateComment(CommentWriteDto comment, Guid commentId, Guid userId);
        public Task<CommentReturnDto> CreateComment(CommentWriteDto comment, Guid userId, Guid postId);
        public Task DeleteComment(Guid commentId, Guid userId);
    }
}
