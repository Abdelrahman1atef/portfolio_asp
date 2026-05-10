using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.DTOs;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class TestimonialsController : ControllerBase
{
    private readonly ITestimonialService _testimonialService;

    public TestimonialsController(ITestimonialService testimonialService)
    {
        _testimonialService = testimonialService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTestimonials()
    {
        var testimonials = await _testimonialService.GetAllAsync();
        return Ok(testimonials);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTestimonial(int id)
    {
        var testimonial = await _testimonialService.GetByIdAsync(id);
        if (testimonial == null) return NotFound(new { message = "Testimonial not found" });
        return Ok(testimonial);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateTestimonial([FromBody] CreateTestimonialRequest request)
    {
        var result = await _testimonialService.CreateAsync(request);
        return CreatedAtAction(nameof(GetTestimonial), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateTestimonial(int id, [FromBody] UpdateTestimonialRequest request)
    {
        var result = await _testimonialService.UpdateAsync(id, request);
        if (result == null) return NotFound(new { message = "Testimonial not found" });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteTestimonial(int id)
    {
        var deleted = await _testimonialService.DeleteAsync(id);
        if (!deleted) return NotFound(new { message = "Testimonial not found" });
        return Ok(new { message = "Testimonial deleted" });
    }
}
