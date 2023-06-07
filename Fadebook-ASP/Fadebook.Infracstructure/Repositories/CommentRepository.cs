using Fadebook.Application.Interfaces.Repositories;
using Fadebook.Application.Models.CommentModel;
using Fadebook.Application.Models.UserModel;
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
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        private ApplicationContext _context;
        public CommentRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Comment>> GetCommentsOfPost(Guid id)
        {
            return await _context.Comments.Where(c => c.PostId == id)
                .Include(c => c.User)
                .ToListAsync();

        }
    }
}
