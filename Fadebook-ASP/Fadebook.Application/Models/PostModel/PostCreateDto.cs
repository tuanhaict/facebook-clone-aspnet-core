using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Models.PostModel
{
    public class PostCreateDto
    {
        public string Caption { get; set; }
        public IFormFile Image { get; set; }
    }
}
