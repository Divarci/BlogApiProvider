using EntityLayer.Blog.DTOs.ArticleDTOs;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Helpers.Image
{
    public interface IImageHelper
    {
        Task<ArticleImageDTO> ImageUpload(IFormFile imageFile);
        string Delete(string imageName);
    }
}
