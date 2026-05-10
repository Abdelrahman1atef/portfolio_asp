using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.DTOs;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBlogs()
    {
        var blogs = await _blogService.GetAllAsync();
        return Ok(blogs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlog(int id)
    {
        var blog = await _blogService.GetByIdAsync(id);
        if (blog == null) return NotFound(new { message = "Blog post not found" });
        return Ok(blog);
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBlogBySlug(string slug)
    {
        var blog = await _blogService.GetBySlugAsync(slug);
        if (blog == null) return NotFound(new { message = "Blog post not found" });
        return Ok(blog);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateBlog([FromBody] CreateBlogRequest request)
    {
        var result = await _blogService.CreateAsync(request);
        return CreatedAtAction(nameof(GetBlog), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateBlog(int id, [FromBody] UpdateBlogRequest request)
    {
        var result = await _blogService.UpdateAsync(id, request);
        if (result == null) return NotFound(new { message = "Blog post not found" });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteBlog(int id)
    {
        var deleted = await _blogService.DeleteAsync(id);
        if (!deleted) return NotFound(new { message = "Blog post not found" });
        return Ok(new { message = "Blog post deleted" });
    }
}
