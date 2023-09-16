using EntityLayer.Blog.DTOs.CategoryDTOs;

namespace EntityLayer.Blog.DTOs.ArticleDTOs
{
    public class ArticleListDTO
    {
        //Primary Key
        public int Id { get; set; }

        //Information
        public string CreatedDate { get; set; } =DateTime.Now.ToString("d");
        public string? UpdatedDate { get; set; }

        //Article Section
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string? YoutubeUrl { get; set; }


        //Picture Section
        public string FileName { get; set; } = null!;
        public byte[] FileByte { get; set; } = null!;
        public string FileType { get; set; } = null!;

        //category relation
        public CategoryDTO Category { get; set; } = null!;
    }
}
