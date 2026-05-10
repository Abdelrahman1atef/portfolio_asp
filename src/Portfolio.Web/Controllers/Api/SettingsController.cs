using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.DTOs;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly ISettingService _settingService;

    public SettingsController(ISettingService settingService)
    {
        _settingService = settingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSettings()
    {
        var settings = await _settingService.GetSettingsAsync();
        if (settings == null) return NotFound(new { message = "Settings not found" });
        return Ok(settings);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateSettings([FromBody] UpdateSettingRequest request)
    {
        var result = await _settingService.UpdateSettingsAsync(request);
        return Ok(result);
    }
}
