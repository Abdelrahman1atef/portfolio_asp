using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.Models;

namespace Portfolio.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private readonly AppDbContext _context;

    public BlogController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetBlogs()
    {
        var blogs = await _context.Blogs.OrderByDescending(b => b.PublishDate).ToListAsync();
        return Ok(blogs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlog(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null) return NotFound(new { message = "Blog post not found" });
        return Ok(blog);
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBlogBySlug(string slug)
    {
        var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Slug == slug && b.IsPublished);
        if (blog == null) return NotFound(new { message = "Blog post not found" });
        return Ok(blog);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateBlog([FromBody] Blog blog)
    {
        if (await _context.Blogs.AnyAsync(b => b.Slug == blog.Slug))
            return BadRequest(new { message = "A blog post with this slug already exists" });

        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBlog), new { id = blog.Id }, blog);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateBlog(int id, [FromBody] Blog blogUpdate)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null) return NotFound(new { message = "Blog post not found" });

        blog.Title = blogUpdate.Title;
        blog.Slug = blogUpdate.Slug;
        blog.Content = blogUpdate.Content;
        blog.Preview = blogUpdate.Preview;
        blog.Tags = blogUpdate.Tags;
        blog.CoverImage = blogUpdate.CoverImage;
        blog.PublishDate = blogUpdate.PublishDate;
        blog.IsPublished = blogUpdate.IsPublished;

        await _context.SaveChangesAsync();
        return Ok(blog);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteBlog(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null) return NotFound(new { message = "Blog post not found" });

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Blog post deleted" });
    }
}
