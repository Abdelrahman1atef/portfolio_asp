using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Portfolio.Web.Models;

public class Project
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string FeaturesJson { get; set; } = "[]";
    public string TechStackJson { get; set; } = "[]";
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
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [NotMapped]
    public List<string> Features
    {
        get => string.IsNullOrEmpty(FeaturesJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(FeaturesJson) ?? new List<string>();
        set => FeaturesJson = JsonSerializer.Serialize(value);
    }

    [NotMapped]
    public List<string> TechStack
    {
        get => string.IsNullOrEmpty(TechStackJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(TechStackJson) ?? new List<string>();
        set => TechStackJson = JsonSerializer.Serialize(value);
    }
}
