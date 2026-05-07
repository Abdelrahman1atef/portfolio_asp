using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.Models;

namespace Portfolio.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class TestimonialsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TestimonialsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTestimonials()
    {
        var testimonials = await _context.Testimonials.OrderBy(t => t.Order).ToListAsync();
        return Ok(testimonials);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateTestimonial([FromBody] Testimonial testimonial)
    {
        _context.Testimonials.Add(testimonial);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTestimonials), new { id = testimonial.Id }, testimonial);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateTestimonial(int id, [FromBody] Testimonial testimonialUpdate)
    {
        var testimonial = await _context.Testimonials.FindAsync(id);
        if (testimonial == null) return NotFound(new { message = "Testimonial not found" });

        testimonial.Name = testimonialUpdate.Name;
        testimonial.Role = testimonialUpdate.Role;
        testimonial.Company = testimonialUpdate.Company;
        testimonial.Quote = testimonialUpdate.Quote;
        testimonial.Avatar = testimonialUpdate.Avatar;
        testimonial.Order = testimonialUpdate.Order;

        await _context.SaveChangesAsync();
        return Ok(testimonial);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteTestimonial(int id)
    {
        var testimonial = await _context.Testimonials.FindAsync(id);
        if (testimonial == null) return NotFound(new { message = "Testimonial not found" });

        _context.Testimonials.Remove(testimonial);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Testimonial deleted" });
    }
}
