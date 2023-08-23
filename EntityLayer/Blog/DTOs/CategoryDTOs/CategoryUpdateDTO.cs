namespace EntityLayer.Blog.DTOs.CategoryDTOs
{
    public class CategoryUpdateDTO
    {
        //category section
        public string Name { get; set; } = null!;

        //Primary Key
        public int Id { get; set; }

        //Information
        public string? UpdatedDate { get; set; }

        //Check Constraint       
        public byte[] RowVersion { get; set; } = null!;
    }
}
