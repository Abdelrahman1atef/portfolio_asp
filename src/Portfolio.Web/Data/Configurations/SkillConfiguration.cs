using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Web.Models;

namespace Portfolio.Web.Data.Configurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.HasMany(s => s.Skills)
            .WithOne(si => si.Skill)
            .HasForeignKey(si => si.SkillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
