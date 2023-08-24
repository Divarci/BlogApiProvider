using EntityLayer.Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.Blog
{
    public class ArticleConfig : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            //Article Section
            builder.Property(x=>x.Title).IsRequired().HasMaxLength(150);
            builder.Property(x=>x.Author).IsRequired().HasMaxLength(70);
            builder.Property(x=>x.Content).IsRequired();

            //Check Constraint Settings
            builder.Property(x => x.RowVersion).IsRowVersion();

            //Information Settings
            builder.Property(x => x.CreatedDate).HasMaxLength(10).IsRequired();
            builder.Property(x => x.UpdatedDate).HasMaxLength(10);

            //Picture Settings
            builder.Property(x => x.FileName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.FileType).HasMaxLength(30).IsRequired();

        }
    }
}
