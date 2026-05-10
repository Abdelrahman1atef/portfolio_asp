using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.DTOs;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public interface IAboutService
{
    Task<AboutDto?> GetAboutAsync();
    Task<AboutDto> UpdateAboutAsync(UpdateAboutRequest request);
}

public class AboutService : IAboutService
{
    private readonly AppDbContext _context;

    public AboutService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AboutDto?> GetAboutAsync()
    {
        var about = await _context.Abouts
            .Include(a => a.Stats)
            .FirstOrDefaultAsync();

        return about != null ? MapToDto(about) : null;
    }

    public async Task<AboutDto> UpdateAboutAsync(UpdateAboutRequest request)
    {
        var about = await _context.Abouts
            .Include(a => a.Stats)
            .FirstOrDefaultAsync();

        if (about == null)
        {
            about = new About();
            _context.Abouts.Add(about);
        }

        about.Bio = request.Bio;
        about.ProfileImage = request.ProfileImage;
        about.Title = request.Title;
        about.Subtitle = request.Subtitle;

        // Sync Stats
        about.Stats.Clear();
        foreach (var stat in request.Stats)
        {
            about.Stats.Add(new AboutStat
            {
                Label = stat.Label,
                Value = stat.Value
            });
        }

        await _context.SaveChangesAsync();
        return MapToDto(about);
    }

    private static AboutDto MapToDto(About about)
    {
        return new AboutDto
        {
            Id = about.Id,
            Bio = about.Bio,
            ProfileImage = about.ProfileImage,
            Title = about.Title,
            Subtitle = about.Subtitle,
            Stats = about.Stats.Select(s => new AboutStatDto
            {
                Id = s.Id,
                Label = s.Label,
                Value = s.Value
            }).ToList(),
            CreatedAt = about.CreatedAt,
            UpdatedAt = about.UpdatedAt
        };
    }
}
