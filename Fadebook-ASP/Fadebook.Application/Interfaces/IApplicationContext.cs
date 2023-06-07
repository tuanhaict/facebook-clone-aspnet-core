using Fadebook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Interfaces
{
    public interface IApplicationContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Introduction> Introductions { get; set; }
        DbSet<Reaction> Reactions { get; set; }
        DbSet<Friend> Friends { get; set; }
    }
}
