using CoreLayer.BaseEntity;

namespace EntityLayer.Blog.Entities
{
    public class Article : BaseEntity
    {
        //Article Section
        public string Title { get; set; } = "x";
        public string Content { get; set; } = "y";
        public string Author { get; set; } = "z";

        //Picture Section
        public string FileName { get; set; } = null!;
        public string FileType { get; set; } = null!;
    }
}
