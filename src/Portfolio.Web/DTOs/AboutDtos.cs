using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.DTOs;

public class AboutDto
{
    public int Id { get; set; }
    public string Bio { get; set; } = string.Empty;
    public string ProfileImage { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public List<AboutStatDto> Stats { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class AboutStatDto
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
}

public class CreateAboutRequest
{
    public string Bio { get; set; } = string.Empty;
    public string ProfileImage { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public List<AboutStatDto> Stats { get; set; } = new();
}

public class UpdateAboutRequest
{
    public string Bio { get; set; } = string.Empty;
    public string ProfileImage { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public List<AboutStatDto> Stats { get; set; } = new();
}
