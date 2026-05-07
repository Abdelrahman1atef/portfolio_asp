using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Web.Models;

namespace Portfolio.Web.Data.Configurations;

public class AboutConfiguration : IEntityTypeConfiguration<About>
{
    public void Configure(EntityTypeBuilder<About> builder)
    {
        builder.HasMany(a => a.Stats)
            .WithOne(s => s.About)
            .HasForeignKey(s => s.AboutId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
