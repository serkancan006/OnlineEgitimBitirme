using BusinessLayer.Abstract.ExternalService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace BusinessLayer.Concrete.ExternalService
{
    public class FileOperationsManager : IFileOperationsService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        public FileOperationsManager(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _configuration = configuration;
        }

        public async Task<(string fileName, string filePath)> SaveFileAsync(IFormFile file, string path)
        {
            try
            {
                var rootPath = _environment.WebRootPath;
                var fullPath = Path.Combine(rootPath, path);
                var fileName = GetUniqueFileName(file.FileName);
                var filePath = Path.Combine(fullPath, fileName);
                var databasePath = path + fileName;

                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return (fileName, databasePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya kaydetme hatası: {ex.Message}");
            }
        }

        public bool DeleteFile(string filePath)
        {
            try
            {
                var fullPath = Path.Combine(_environment.WebRootPath, filePath);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Dosya silme hatası: {ex.Message}");
            }
        }

        public string GetFileConvertUrl(string imageUrl)
        {
            return "https://" + _configuration["BaseUrl"] + imageUrl;
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            string fileExtension = Path.GetExtension(fileName);
            string pureFileName = Path.GetFileNameWithoutExtension(fileName);

            pureFileName = Regex.Replace(pureFileName, "[^a-zA-Z0-9]", "-");

            string newFileName = pureFileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExtension;
            return newFileName;
        }

    }
}
