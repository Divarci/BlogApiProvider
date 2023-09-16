using EntityLayer.Blog.DTOs.ArticleDTOs;

namespace EntityLayer.Blog.DTOs.CategoryDTOs
{
    public class CategoryListDTO
    {
        //Primary Key
        public int Id { get; set; }

        //Information
        public string CreatedDate { get; set; } = DateTime.Now.ToString("d");
        public string? UpdatedDate { get; set; }

        //category section
        public string Name { get; set; } = null!;

        //Article List
        public List<ArticleListDTO>? Articles { get; set; }

    }
}
