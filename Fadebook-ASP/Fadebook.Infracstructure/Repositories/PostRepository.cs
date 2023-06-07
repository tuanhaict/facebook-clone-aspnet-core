using Fadebook.Application.Interfaces.Repositories;
using Fadebook.Domain.Entities;
using Fadebook.Infracstructure.Data;
using Fadebook.Infracstructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Infracstructure.Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        private ApplicationContext _context;
        public PostRepository(ApplicationContext context): base(context)
        {
            _context = context;
        }

        public async Task<Post> GetPost(Guid id)
        {
            return await _context.Posts.Include(p=> p.Comments)
                .Include(p => p.User)
                .Include(p=> p.Reactions)
                .FirstOrDefaultAsync(p => p.Id ==id);

        }

        public async Task<IReadOnlyList<Post>> GetPosts()
        {
            return await _context.Posts.Include(c => c.Comments)
                .Include(c => c.User)
                .Include(c => c.Reactions)
                .Take(20)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Post>> GetPostsOfUser(Guid userId)
        {
            return await _context.Posts.Include(c=> c.Comments)
                    .Include(c => c.User)
                    .Include(c => c.Reactions)
                    .Where(p => p.Id == userId)
                    .ToListAsync();
        }
    }
}
