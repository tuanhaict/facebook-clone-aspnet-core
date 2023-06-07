using Fadebook.Application.Models.PostModel;
using Fadebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Services.Interfaces
{
    public interface IPostService
    {
        public Task<PostReturnDto> CreatePost(PostCreateDto post, Guid userId);
        public Task<PostReturnDto> GetPost(Guid id);
        public Task<IReadOnlyList<PostReturnDto>> GetPosts();
        public Task UpdatePost(PostUpdateDto post, Guid id, Guid userId);
        public Task DeletePost(Guid id, Guid userId);
        public Task ToogleLikePost(Guid userId, Guid postId);
    }
}
