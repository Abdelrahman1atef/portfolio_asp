using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.DTOs;

public class TestimonialDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Quote { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public int Order { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateTestimonialRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    [Required]
    public string Quote { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public int Order { get; set; }
}

public class UpdateTestimonialRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    [Required]
    public string Quote { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public int Order { get; set; }
}
