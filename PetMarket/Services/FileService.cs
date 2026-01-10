using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PetMarket.Services.Interfaces;

namespace PetMarket.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string subDirectory)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            string uploadDir = Path.Combine(_env.WebRootPath, "images", subDirectory);
            Directory.CreateDirectory(uploadDir);

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/images/{subDirectory}/{fileName}";
        }

        public void DeleteFile(string filePathUrl)
        {
            if (string.IsNullOrEmpty(filePathUrl))
                return;

            var fullPath = Path.Combine(_env.WebRootPath, filePathUrl.TrimStart('/'));

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}