using Microsoft.AspNetCore.Http;

namespace EntityLayer.Blog.DTOs.ArticleDTOs
{
    public class ArticleAddDTO
    {               
        //Article Section
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Author { get; set; } = null!;

        //Picture Section
        public string FileName { get; set; } = null!;
        public string FileType { get; set; } = null!;

        //Photo Add
        public IFormFile Photo { get; set; } = null!;
    }
}
