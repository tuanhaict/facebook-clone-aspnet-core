using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fadebook.Domain.Entities.Base;

namespace Fadebook.Domain.Entities
{
    public class Reaction : IContainsUserId
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }  
    }
}
