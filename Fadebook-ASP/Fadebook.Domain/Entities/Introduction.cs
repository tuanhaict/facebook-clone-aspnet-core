using Fadebook.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Domain.Entities
{
    public class Introduction
    {
        [ForeignKey(nameof(User))]
        public Guid Id { get; set; }
        public string? Address { get; set; }
        public string? Job { get; set; }
        public string? Company { get; set; }
        public User User { get; set; }

    }
}
