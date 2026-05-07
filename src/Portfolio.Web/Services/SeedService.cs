using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.Helpers;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public class SeedService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public SeedService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task SeedAsync()
    {
        // Try creating database if it doesn't exist
        try {
            await _context.Database.MigrateAsync();
        } catch {
            // Ignore in case EF migration hasn't been run or DB is empty
            await _context.Database.EnsureCreatedAsync();
        }

        var adminEmail = _configuration["Admin:Email"] ?? "admin@admin.com";
        var adminPassword = _configuration["Admin:Password"] ?? "admin";

        if (!await _context.Users.AnyAsync(u => u.Email == adminEmail))
        {
            _context.Users.Add(new User
            {
                Name = "Admin",
                Email = adminEmail,
                Password = PasswordHelper.HashPassword(adminPassword),
                Role = "admin"
            });
        }

        if (!await _context.Abouts.AnyAsync())
        {
            var about = new About
            {
                Bio = "I didn't choose Flutter randomly—it came after exploring different technologies and realizing I enjoy building complete products, not just writing code.",
                ProfileImage = "/images/me.jpg",
                Title = "Flutter Developer building scalable, high-quality mobile apps",
                Subtitle = "Focused on clean architecture, performance, and user experience.",
                Stats = new List<AboutStat>
                {
                    new AboutStat { Value = "1+", Label = "Years Experience" },
                    new AboutStat { Value = "5+", Label = "Projects Completed" },
                    new AboutStat { Value = "100%", Label = "Commitment" },
                    new AboutStat { Value = "∞", Label = "Lines of Code" }
                }
            };
            _context.Abouts.Add(about);
        }

        if (!await _context.Settings.AnyAsync())
        {
            _context.Settings.Add(new Setting());
        }

        if (!await _context.Projects.AnyAsync())
        {
            _context.Projects.Add(new Project
            {
                Slug = "abher",
                Title = "Abher",
                ShortDescription = "Boat booking platform for trips and marine events.",
                Description = "A comprehensive platform for booking boats, marine trips, and sea events.",
                Features = new List<string> { "Dual application support", "Real-time booking", "Multi-environment setup" },
                TechStack = new List<string> { "Flutter", "Dart", "Cubit", "Dio", "Google Maps", "Firebase" },
                Image = "/images/abher.png",
                LiveUrl = "https://play.google.com/store/apps/details?id=com.masader.Abhr",
                Category = "Mobile App",
                Order = 1
            });
        }

        await _context.SaveChangesAsync();
    }
}
