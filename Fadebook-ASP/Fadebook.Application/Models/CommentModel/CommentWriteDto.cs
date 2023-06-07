using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Models.CommentModel
{
    public class CommentWriteDto
    {
        [Required]
        public string Content { get; set; }
    }
}
