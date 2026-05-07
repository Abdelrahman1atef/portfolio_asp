using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.Models;

namespace Portfolio.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly AppDbContext _context;

    public MessagesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetMessages()
    {
        var messages = await _context.Messages.OrderByDescending(m => m.CreatedAt).ToListAsync();
        return Ok(messages);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage([FromBody] Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMessages), new { id = message.Id }, message);
    }

    [HttpPut("{id}/read")]
    [Authorize]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var message = await _context.Messages.FindAsync(id);
        if (message == null) return NotFound(new { message = "Message not found" });

        message.IsRead = true;
        await _context.SaveChangesAsync();
        return Ok(message);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteMessage(int id)
    {
        var message = await _context.Messages.FindAsync(id);
        if (message == null) return NotFound(new { message = "Message not found" });

        _context.Messages.Remove(message);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Message deleted" });
    }
}
