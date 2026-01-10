
namespace PetMarket.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string subDirectory);
        void DeleteFile(string filePathUrl);
    }
}