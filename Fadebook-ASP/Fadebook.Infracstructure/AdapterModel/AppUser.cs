using Fadebook.Domain.Entities.Enums;
using Fadebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Fadebook.Infracstructure.AdapterModel
{
    public class AppUser : IdentityUser<Guid>
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
        public Guid IntroductionId { get; set; }
        public Introduction Introduction { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
