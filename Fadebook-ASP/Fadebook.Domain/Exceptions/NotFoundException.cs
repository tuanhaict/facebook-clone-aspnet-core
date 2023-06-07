using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Domain.Exceptions
{
    public class NotFoundException : BaseException
    {
        private readonly string _message;
        public NotFoundException(string message) : base(message) => _message = message;
        public override int StatusCode => 404;
        public override string Message => _message;
    }
}
