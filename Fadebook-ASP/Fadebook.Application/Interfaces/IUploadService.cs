using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fadebook.Application.Interfaces
{
    public interface IUploadService
    {
        public Task<string> UploadImage(IFormFile file, string type);
    }
}
