using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Domain.Exceptions
{
    public class BadRequestException : BaseException
    {
        private readonly string _message;
        public BadRequestException(string message) : base(message) => _message = message;
        public override int StatusCode => 400;
        public override string Message => _message;
    }
}
