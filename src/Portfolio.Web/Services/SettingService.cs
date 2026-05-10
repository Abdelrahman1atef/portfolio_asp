using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.DTOs;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public interface ISettingService
{
    Task<SettingDto?> GetSettingsAsync();
    Task<SettingDto> UpdateSettingsAsync(UpdateSettingRequest request);
}

public class SettingService : ISettingService
{
    private readonly AppDbContext _context;

    public SettingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<SettingDto?> GetSettingsAsync()
    {
        var settings = await _context.Settings.FirstOrDefaultAsync();
        return settings != null ? MapToDto(settings) : null;
    }

    public async Task<SettingDto> UpdateSettingsAsync(UpdateSettingRequest request)
    {
        var settings = await _context.Settings.FirstOrDefaultAsync();

        if (settings == null)
        {
            settings = new Setting();
            _context.Settings.Add(settings);
        }

        settings.Name = request.Name;
        settings.Email = request.Email;
        settings.Phone = request.Phone;
        settings.GithubUrl = request.GithubUrl;
        settings.LinkedinUrl = request.LinkedinUrl;
        settings.WhatsappUrl = request.WhatsappUrl;
        settings.CvFile = request.CvFile;
        settings.ProfileImage = request.ProfileImage;
        settings.ThemePreference = request.ThemePreference;

        await _context.SaveChangesAsync();
        return MapToDto(settings);
    }

    private static SettingDto MapToDto(Setting settings)
    {
        return new SettingDto
        {
            Id = settings.Id,
            Name = settings.Name,
            Email = settings.Email,
            Phone = settings.Phone,
            GithubUrl = settings.GithubUrl,
            LinkedinUrl = settings.LinkedinUrl,
            WhatsappUrl = settings.WhatsappUrl,
            CvFile = settings.CvFile,
            ProfileImage = settings.ProfileImage,
            ThemePreference = settings.ThemePreference,
            CreatedAt = settings.CreatedAt,
            UpdatedAt = settings.UpdatedAt
        };
    }
}
