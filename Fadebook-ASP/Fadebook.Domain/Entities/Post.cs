using Fadebook.Domain.Common;
using Fadebook.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Domain.Entities
{
    public class Post : IContainsUserId
    {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public string? Image { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Reaction> Reactions { get; set; }
    }
}
