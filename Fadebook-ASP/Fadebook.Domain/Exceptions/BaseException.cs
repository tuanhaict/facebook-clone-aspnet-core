using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Domain.Exceptions
{
    public abstract class BaseException : Exception
    {
        public BaseException(string message): base(message) { }
        public abstract int StatusCode { get; }
        public abstract string Message { get; }
    }
}
