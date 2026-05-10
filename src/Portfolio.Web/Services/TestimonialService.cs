using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.DTOs;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public interface ITestimonialService
{
    Task<List<TestimonialDto>> GetAllAsync();
    Task<TestimonialDto?> GetByIdAsync(int id);
    Task<TestimonialDto> CreateAsync(CreateTestimonialRequest request);
    Task<TestimonialDto?> UpdateAsync(int id, UpdateTestimonialRequest request);
    Task<bool> DeleteAsync(int id);
}

public class TestimonialService : ITestimonialService
{
    private readonly AppDbContext _context;

    public TestimonialService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TestimonialDto>> GetAllAsync()
    {
        var testimonials = await _context.Testimonials
            .OrderBy(t => t.Order)
            .ToListAsync();

        return testimonials.Select(MapToDto).ToList();
    }

    public async Task<TestimonialDto?> GetByIdAsync(int id)
    {
        var testimonial = await _context.Testimonials.FindAsync(id);
        return testimonial != null ? MapToDto(testimonial) : null;
    }

    public async Task<TestimonialDto> CreateAsync(CreateTestimonialRequest request)
    {
        var testimonial = new Testimonial
        {
            Name = request.Name,
            Role = request.Role,
            Company = request.Company,
            Quote = request.Quote,
            Avatar = request.Avatar,
            Order = request.Order
        };

        _context.Testimonials.Add(testimonial);
        await _context.SaveChangesAsync();

        return MapToDto(testimonial);
    }

    public async Task<TestimonialDto?> UpdateAsync(int id, UpdateTestimonialRequest request)
    {
        var testimonial = await _context.Testimonials.FindAsync(id);
        if (testimonial == null) return null;

        testimonial.Name = request.Name;
        testimonial.Role = request.Role;
        testimonial.Company = request.Company;
        testimonial.Quote = request.Quote;
        testimonial.Avatar = request.Avatar;
        testimonial.Order = request.Order;

        await _context.SaveChangesAsync();
        return MapToDto(testimonial);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var testimonial = await _context.Testimonials.FindAsync(id);
        if (testimonial == null) return false;

        _context.Testimonials.Remove(testimonial);
        await _context.SaveChangesAsync();
        return true;
    }

    private static TestimonialDto MapToDto(Testimonial testimonial)
    {
        return new TestimonialDto
        {
            Id = testimonial.Id,
            Name = testimonial.Name,
            Role = testimonial.Role,
            Company = testimonial.Company,
            Quote = testimonial.Quote,
            Avatar = testimonial.Avatar,
            Order = testimonial.Order,
            CreatedAt = testimonial.CreatedAt,
            UpdatedAt = testimonial.UpdatedAt
        };
    }
}
