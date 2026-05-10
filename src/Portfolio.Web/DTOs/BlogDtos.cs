using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.DTOs;

public class BlogDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Preview { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public string CoverImage { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateBlogRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Slug { get; set; } = string.Empty;
    [Required]
    public string Content { get; set; } = string.Empty;
    public string Preview { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public string CoverImage { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
}

public class UpdateBlogRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Slug { get; set; } = string.Empty;
    [Required]
    public string Content { get; set; } = string.Empty;
    public string Preview { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public string CoverImage { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
}
