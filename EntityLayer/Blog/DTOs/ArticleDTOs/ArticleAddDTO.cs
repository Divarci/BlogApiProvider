using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public IFormFile Photo { get; set; } = null!;

        //category relation
        public int CategoryId { get; set; }
    }
}
