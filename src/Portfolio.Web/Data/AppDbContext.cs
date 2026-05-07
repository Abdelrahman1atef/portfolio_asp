using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Models;

namespace Portfolio.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Blog> Blogs { get; set; } = null!;
    public DbSet<Skill> Skills { get; set; } = null!;
    public DbSet<SkillItem> SkillItems { get; set; } = null!;
    public DbSet<About> Abouts { get; set; } = null!;
    public DbSet<AboutStat> AboutStats { get; set; } = null!;
    public DbSet<Setting> Settings { get; set; } = null!;
    public DbSet<Testimonial> Testimonials { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            var createdAtProperty = entityEntry.Metadata.FindProperty("CreatedAt");
            var updatedAtProperty = entityEntry.Metadata.FindProperty("UpdatedAt");

            if (entityEntry.State == EntityState.Added && createdAtProperty != null)
            {
                entityEntry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
            }

            if (updatedAtProperty != null)
            {
                entityEntry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}
