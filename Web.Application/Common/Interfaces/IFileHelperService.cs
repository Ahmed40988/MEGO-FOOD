using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Common.Interfaces
{
    public interface IFileHelperService
    {
        string UploadFile(IFormFile file, string folderName);
        void DeleteFile(string fileName, string folderName);
    }
}
