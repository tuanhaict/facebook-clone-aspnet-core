using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Domain.Interfaces
{
    public interface IAppLogger
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message);
    }
}
