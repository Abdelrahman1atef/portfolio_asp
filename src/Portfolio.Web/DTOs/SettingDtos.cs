using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.DTOs;

public class SettingDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string GithubUrl { get; set; } = string.Empty;
    public string LinkedinUrl { get; set; } = string.Empty;
    public string WhatsappUrl { get; set; } = string.Empty;
    public string CvFile { get; set; } = string.Empty;
    public string ProfileImage { get; set; } = string.Empty;
    public string ThemePreference { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class UpdateSettingRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string GithubUrl { get; set; } = string.Empty;
    public string LinkedinUrl { get; set; } = string.Empty;
    public string WhatsappUrl { get; set; } = string.Empty;
    public string CvFile { get; set; } = string.Empty;
    public string ProfileImage { get; set; } = string.Empty;
    public string ThemePreference { get; set; } = string.Empty;
}
