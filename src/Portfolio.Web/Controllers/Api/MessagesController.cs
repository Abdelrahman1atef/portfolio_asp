using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.DTOs;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessagesController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetMessages()
    {
        var messages = await _messageService.GetAllAsync();
        return Ok(messages);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetMessage(int id)
    {
        var message = await _messageService.GetByIdAsync(id);
        if (message == null) return NotFound(new { message = "Message not found" });
        return Ok(message);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage([FromBody] SendMessageRequest request)
    {
        var result = await _messageService.SendAsync(request);
        return CreatedAtAction(nameof(GetMessage), new { id = result.Id }, result);
    }

    [HttpPut("{id}/read")]
    [Authorize]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var success = await _messageService.MarkAsReadAsync(id);
        if (!success) return NotFound(new { message = "Message not found" });
        return Ok(new { message = "Message marked as read" });
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteMessage(int id)
    {
        var success = await _messageService.DeleteAsync(id);
        if (!success) return NotFound(new { message = "Message not found" });
        return Ok(new { message = "Message deleted" });
    }
}
