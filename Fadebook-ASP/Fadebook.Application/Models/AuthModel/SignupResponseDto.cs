using Fadebook.Domain.Entities;
using Fadebook.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Models.AuthModel
{
    public class SignupResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public Introduction Introduction { get; set; }

    }
}
