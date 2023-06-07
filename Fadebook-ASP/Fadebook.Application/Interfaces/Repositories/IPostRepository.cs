using Fadebook.Application.Interfaces.Repositories.Base;
using Fadebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Interfaces.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IReadOnlyList<Post>> GetPostsOfUser(Guid userId);
        Task<Post> GetPost(Guid id);
        Task<IReadOnlyList<Post>> GetPosts();
    }
}
