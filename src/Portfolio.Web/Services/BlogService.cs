using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.DTOs;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public interface IBlogService
{
    Task<List<BlogDto>> GetAllAsync(bool includeUnpublished = false);
    Task<BlogDto?> GetByIdAsync(int id);
    Task<BlogDto?> GetBySlugAsync(string slug);
    Task<BlogDto> CreateAsync(CreateBlogRequest request);
    Task<BlogDto?> UpdateAsync(int id, UpdateBlogRequest request);
    Task<bool> DeleteAsync(int id);
}

public class BlogService : IBlogService
{
    private readonly AppDbContext _context;

    public BlogService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BlogDto>> GetAllAsync(bool includeUnpublished = false)
    {
        var query = _context.Blogs.AsQueryable();
        if (!includeUnpublished)
        {
            query = query.Where(b => b.IsPublished);
        }

        var blogs = await query.OrderByDescending(b => b.PublishDate).ToListAsync();
        return blogs.Select(MapToDto).ToList();
    }

    public async Task<BlogDto?> GetByIdAsync(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        return blog != null ? MapToDto(blog) : null;
    }

    public async Task<BlogDto?> GetBySlugAsync(string slug)
    {
        var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Slug == slug && b.IsPublished);
        return blog != null ? MapToDto(blog) : null;
    }

    public async Task<BlogDto> CreateAsync(CreateBlogRequest request)
    {
        var blog = new Blog
        {
            Title = request.Title,
            Slug = request.Slug,
            Content = request.Content,
            Preview = request.Preview,
            Tags = request.Tags,
            CoverImage = request.CoverImage,
            IsPublished = request.IsPublished,
            PublishDate = request.IsPublished ? DateTime.UtcNow : DateTime.MinValue
        };

        _context.Blogs.Add(blog);
        await _context.SaveChangesAsync();

        return MapToDto(blog);
    }

    public async Task<BlogDto?> UpdateAsync(int id, UpdateBlogRequest request)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null) return null;

        blog.Title = request.Title;
        blog.Slug = request.Slug;
        blog.Content = request.Content;
        blog.Preview = request.Preview;
        blog.Tags = request.Tags;
        blog.CoverImage = request.CoverImage;
        
        if (!blog.IsPublished && request.IsPublished)
        {
            blog.PublishDate = DateTime.UtcNow;
        }
        blog.IsPublished = request.IsPublished;

        await _context.SaveChangesAsync();
        return MapToDto(blog);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null) return false;

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();
        return true;
    }

    private static BlogDto MapToDto(Blog blog)
    {
        return new BlogDto
        {
            Id = blog.Id,
            Title = blog.Title,
            Slug = blog.Slug,
            Content = blog.Content,
            Preview = blog.Preview,
            Tags = blog.Tags,
            CoverImage = blog.CoverImage,
            PublishDate = blog.PublishDate,
            IsPublished = blog.IsPublished,
            CreatedAt = blog.CreatedAt,
            UpdatedAt = blog.UpdatedAt
        };
    }
}
