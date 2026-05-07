using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Web.Models;

namespace Portfolio.Web.Data.Configurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.HasIndex(b => b.Slug).IsUnique();
        builder.Property(b => b.Slug).IsRequired().HasMaxLength(200);
        builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
    }
}
