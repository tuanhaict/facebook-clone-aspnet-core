using Fadebook.Application.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Models.PostModel
{
    public class PostReturnDto
    {
        public Guid Id { get; set; }
        public string Caption { get; set; }
        public string Image { get; set; }
        public UserForDisplayDto User { get; set; }
        public short Comments { get; set; }
        public short Reactions { get; set; }
    }
}
