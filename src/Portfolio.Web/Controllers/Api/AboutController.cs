using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.DTOs;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class AboutController : ControllerBase
{
    private readonly IAboutService _aboutService;

    public AboutController(IAboutService aboutService)
    {
        _aboutService = aboutService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAbout()
    {
        var about = await _aboutService.GetAboutAsync();
        if (about == null) return NotFound(new { message = "About information not found" });
        return Ok(about);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAbout([FromBody] UpdateAboutRequest request)
    {
        var result = await _aboutService.UpdateAboutAsync(request);
        return Ok(result);
    }
}
