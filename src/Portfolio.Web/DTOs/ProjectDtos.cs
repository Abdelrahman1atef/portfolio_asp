using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.DTOs;

public class ProjectDto
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Features { get; set; } = new();
    public List<string> TechStack { get; set; } = new();
    public string Image { get; set; } = string.Empty;
    public string LiveUrl { get; set; } = string.Empty;
    public string GithubUrl { get; set; } = string.Empty;
    public string Problem { get; set; } = string.Empty;
    public string Solution { get; set; } = string.Empty;
    public string Architecture { get; set; } = string.Empty;
    public string Challenges { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateProjectRequest
{
    [Required]
    public string Slug { get; set; } = string.Empty;
    [Required]
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Features { get; set; } = new();
    public List<string> TechStack { get; set; } = new();
    public string Image { get; set; } = string.Empty;
    public string LiveUrl { get; set; } = string.Empty;
    public string GithubUrl { get; set; } = string.Empty;
    public string Problem { get; set; } = string.Empty;
    public string Solution { get; set; } = string.Empty;
    public string Architecture { get; set; } = string.Empty;
    public string Challenges { get; set; } = string.Empty;
    public string Category { get; set; } = "Mobile App";
    public int Order { get; set; }
    public bool IsPublished { get; set; } = true;
}

public class UpdateProjectRequest
{
    [Required]
    public string Slug { get; set; } = string.Empty;
    [Required]
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Features { get; set; } = new();
    public List<string> TechStack { get; set; } = new();
    public string Image { get; set; } = string.Empty;
    public string LiveUrl { get; set; } = string.Empty;
    public string GithubUrl { get; set; } = string.Empty;
    public string Problem { get; set; } = string.Empty;
    public string Solution { get; set; } = string.Empty;
    public string Architecture { get; set; } = string.Empty;
    public string Challenges { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool IsPublished { get; set; }
}
