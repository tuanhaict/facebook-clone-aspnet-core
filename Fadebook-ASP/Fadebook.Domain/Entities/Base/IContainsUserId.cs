using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Domain.Entities.Base
{
    public interface IContainsUserId
    {
        public Guid UserId { get; set; }
    }
}
