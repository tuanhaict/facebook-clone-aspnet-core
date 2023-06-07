using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Models.IntroductionModel
{
    public class IntroductionReadDto
    {
        public Guid Id { get; set; }
        public string? Address { get; set; }
        public string? Job { get; set; }
        public string? Company { get; set; }
    }
}
