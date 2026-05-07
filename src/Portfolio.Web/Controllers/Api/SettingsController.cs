using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.Models;

namespace Portfolio.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SettingsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetSettings()
    {
        var settings = await _context.Settings.FirstOrDefaultAsync();
        if (settings == null)
        {
            settings = new Setting();
            _context.Settings.Add(settings);
            await _context.SaveChangesAsync();
        }
        return Ok(settings);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateSettings([FromBody] Setting settingUpdate)
    {
        var settings = await _context.Settings.FirstOrDefaultAsync();
        if (settings == null)
        {
            _context.Settings.Add(settingUpdate);
        }
        else
        {
            settings.Name = settingUpdate.Name;
            settings.Email = settingUpdate.Email;
            settings.Phone = settingUpdate.Phone;
            settings.GithubUrl = settingUpdate.GithubUrl;
            settings.LinkedinUrl = settingUpdate.LinkedinUrl;
            settings.WhatsappUrl = settingUpdate.WhatsappUrl;
            settings.CvFile = settingUpdate.CvFile;
            settings.ThemePreference = settingUpdate.ThemePreference;
        }

        await _context.SaveChangesAsync();
        return Ok(settings ?? settingUpdate);
    }
}
