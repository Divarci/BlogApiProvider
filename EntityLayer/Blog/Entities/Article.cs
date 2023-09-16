using CoreLayer.BaseEntity;

namespace EntityLayer.Blog.Entities
{
    public class Article : BaseEntity
    {
        //Article Section
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string? YoutubeUrl { get; set; }

        //Picture Section
        public string FileName { get; set; } = null!;
        public string FileType { get; set; } = null!;

        //category relation
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
