using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Portfolio.Web.Models;

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Preview { get; set; } = string.Empty;
    public string TagsJson { get; set; } = "[]";
    public string CoverImage { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; } = DateTime.UtcNow;
    public bool IsPublished { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [NotMapped]
    public List<string> Tags
    {
        get => string.IsNullOrEmpty(TagsJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(TagsJson) ?? new List<string>();
        set => TagsJson = JsonSerializer.Serialize(value);
    }
}
