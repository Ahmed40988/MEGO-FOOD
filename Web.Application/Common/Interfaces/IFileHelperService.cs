namespace Web.Application.Common.Interfaces
{
    public interface IFileHelperService
    {
        string UploadFile(IFormFile file, string folderName);
        void DeleteFile(string fileName, string folderName);
    }
}
