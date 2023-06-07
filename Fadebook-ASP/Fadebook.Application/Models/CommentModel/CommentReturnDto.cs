using Fadebook.Application.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Models.CommentModel
{
    public class CommentReturnDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public UserForDisplayDto User { get; set; }
    }
}
