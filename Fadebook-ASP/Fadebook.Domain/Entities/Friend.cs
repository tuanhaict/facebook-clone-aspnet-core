using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Domain.Entities
{
    public class Friend
    {
        public Guid Id { get; set; }
        [ForeignKey(nameof(FirstUser))]
        public Guid FirstId { get; set; }
        public User FirstUser { get; set; }
        [ForeignKey(nameof(SecondUser))]
        public Guid SecondId { get; set; }
        public User SecondUser { get; set; }
        public bool Accepted { get; set; }
    }
}
