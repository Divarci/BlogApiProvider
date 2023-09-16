using EntityLayer.Blog.DTOs.ArticleDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ServiceLayer.Helpers.Image
{
    public class ImageHelper : IImageHelper
    {
        private readonly IHostEnvironment _environment;
        private const string ArticleFolder = "ArticleImages";
        private readonly string _wwwroot;


        public ImageHelper(IHostEnvironment environment)
        {
            _environment = environment;
            _wwwroot = environment.ContentRootPath + "/wwwroot/";
        }

        public async Task<ArticleImageDTO> ImageUpload(IFormFile imageFile)
        {
            
            if (!Directory.Exists($"{_wwwroot}/{ArticleFolder}"))
                Directory.CreateDirectory($"{_wwwroot}/{ArticleFolder}");

            string fileExtension = Path.GetExtension(imageFile.FileName);

            if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
                return new ArticleImageDTO { Error = "You have to upload a JPG,JPEG or PNG" };

            string dateTime = DateTime.Now.Microsecond.ToString();
            string newFileName = $"Article_{dateTime}{fileExtension}";

            string path = Path.Combine($"{_wwwroot}/{ArticleFolder}", newFileName);

            await using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
            await imageFile.CopyToAsync(stream);
            await stream.FlushAsync();

            return new ArticleImageDTO { FileName = $"{newFileName}",FileType = imageFile.ContentType };
        }
        public string Delete(string imageName)
        {

            var fileToDelete = Path.Combine($"{_wwwroot}/{ArticleFolder}/{imageName}");
            if (File.Exists(fileToDelete)) File.Delete(fileToDelete);

            return "Deleted";
        }
    }
}
