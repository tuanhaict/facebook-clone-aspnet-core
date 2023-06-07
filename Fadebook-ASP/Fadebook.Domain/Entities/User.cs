using Fadebook.Domain.Common;
using Fadebook.Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [NotMapped]
        public string Password { get; set; }
        public string? Avatar { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Introduction Introduction { get; set; }
        public ICollection<Post> Posts { get; set; }
        
    }
}
