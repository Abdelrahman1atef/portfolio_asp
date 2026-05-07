using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.Models;

namespace Portfolio.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class AboutController : ControllerBase
{
    private readonly AppDbContext _context;

    public AboutController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAbout()
    {
        var about = await _context.Abouts.Include(a => a.Stats).FirstOrDefaultAsync();
        if (about == null)
        {
            about = new About();
            _context.Abouts.Add(about);
            await _context.SaveChangesAsync();
        }
        return Ok(about);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAbout([FromBody] About aboutUpdate)
    {
        var about = await _context.Abouts.Include(a => a.Stats).FirstOrDefaultAsync();
        if (about == null)
        {
            _context.Abouts.Add(aboutUpdate);
        }
        else
        {
            about.Bio = aboutUpdate.Bio;
            about.ProfileImage = aboutUpdate.ProfileImage;
            about.Title = aboutUpdate.Title;
            about.Subtitle = aboutUpdate.Subtitle;
            
            _context.AboutStats.RemoveRange(about.Stats);
            about.Stats = aboutUpdate.Stats;
        }

        await _context.SaveChangesAsync();
        return Ok(about ?? aboutUpdate);
    }
}
