using Fadebook.Application.Models.ModelBinder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Models.UserModel
{
    public class UserUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        [ModelBinder(BinderType = typeof(DateModelBinder))]
        public DateTime? DateOfBirth { get; set; }
    }
}
