using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.DTOs;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public interface IMessageService
{
    Task<List<MessageDto>> GetAllAsync();
    Task<MessageDto?> GetByIdAsync(int id);
    Task<MessageDto> SendAsync(SendMessageRequest request);
    Task<bool> MarkAsReadAsync(int id);
    Task<bool> DeleteAsync(int id);
}

public class MessageService : IMessageService
{
    private readonly AppDbContext _context;

    public MessageService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MessageDto>> GetAllAsync()
    {
        var messages = await _context.Messages
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();

        return messages.Select(MapToDto).ToList();
    }

    public async Task<MessageDto?> GetByIdAsync(int id)
    {
        var message = await _context.Messages.FindAsync(id);
        if (message != null && !message.IsRead)
        {
            message.IsRead = true;
            await _context.SaveChangesAsync();
        }
        return message != null ? MapToDto(message) : null;
    }

    public async Task<MessageDto> SendAsync(SendMessageRequest request)
    {
        var message = new Message
        {
            Name = request.Name,
            Email = request.Email,
            Subject = request.Subject,
            Body = request.Body,
            IsRead = false
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        return MapToDto(message);
    }

    public async Task<bool> MarkAsReadAsync(int id)
    {
        var message = await _context.Messages.FindAsync(id);
        if (message == null) return false;

        message.IsRead = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var message = await _context.Messages.FindAsync(id);
        if (message == null) return false;

        _context.Messages.Remove(message);
        await _context.SaveChangesAsync();
        return true;
    }

    private static MessageDto MapToDto(Message message)
    {
        return new MessageDto
        {
            Id = message.Id,
            Name = message.Name,
            Email = message.Email,
            Subject = message.Subject,
            Body = message.Body,
            IsRead = message.IsRead,
            CreatedAt = message.CreatedAt
        };
    }
}
