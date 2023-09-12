using Fadebook.Application.Interfaces;
using Fadebook.Domain.Entities;
using Fadebook.Infracstructure.AdapterModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Infracstructure.Data
{
    public class ApplicationContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationContext(DbContextOptions options) : base(options) 
        { 
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Introduction> Introductions { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Friend> Friends { get; set; }
    }
}
