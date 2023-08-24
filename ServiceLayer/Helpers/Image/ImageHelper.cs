using EntityLayer.Blog.DTOs.ArticleDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ServiceLayer.Helpers.Image
{
    public class ImageHelper : IImageHelper
    {
        private readonly IHostEnvironment _environment;
        private const string ArticleFolder = "ArticleImages";


        public ImageHelper(IHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<ArticleImageDto> ImageUpload(IFormFile imageFile)
        {
            if (!Directory.Exists($"{_environment.ContentRootPath}/{ArticleFolder}"))
                Directory.CreateDirectory($"{_environment.ContentRootPath}/{ArticleFolder}");

            string fileExtension = Path.GetExtension(imageFile.FileName);

            if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
                return new ArticleImageDto { Error = "You have to upload a JPG,JPEG or PNG" };

            string dateTime = DateTime.Now.Microsecond.ToString();
            string newFileName = $"Article_{dateTime}{fileExtension}";

            string path = Path.Combine($"{_environment.ContentRootPath}/{ArticleFolder}", newFileName);

            await using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
            await imageFile.CopyToAsync(stream);
            await stream.FlushAsync();

            return new ArticleImageDto { FileName = $"{ArticleFolder}/{newFileName}" };
        }
        public string Delete(string imageName)
        {

            var fileToDelete = Path.Combine($"{_environment.ContentRootPath}/{imageName}");
            if (File.Exists(fileToDelete)) File.Delete(fileToDelete);

            return "Deleted";
        }
    }
}
