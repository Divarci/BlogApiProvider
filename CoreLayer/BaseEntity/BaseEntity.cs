namespace CoreLayer.BaseEntity
{
    public abstract class BaseEntity
    {
        //Primary Key
        public int Id { get; set; }

        //Information
        public string CreatedDate { get; set; } =DateTime.Now.ToString("d");
        public string? UpdatedDate { get; set; }

        //Check Constraint       
        public byte[] RowVersion { get; set; } = null!;
    }
}
