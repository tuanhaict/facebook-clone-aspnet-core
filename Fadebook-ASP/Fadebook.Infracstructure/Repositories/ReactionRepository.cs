using Fadebook.Application.Interfaces.Repositories;
using Fadebook.Application.Interfaces.Repositories.Base;
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
    public class ReactionRepository : RepositoryBase<Reaction>, IReactionRepository
    {
        private readonly ApplicationContext _context;
        public ReactionRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Reaction> CheckLikePost(Guid userId, Guid postId)
        {
            return await _context.Reactions.FirstOrDefaultAsync(c => (c.UserId == userId) && (c.PostId == postId));
            
        }
    }
}
