using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.DTOs;

public class SkillDto
{
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public int Order { get; set; }
    public List<SkillItemDto> Skills { get; set; } = new();
}

public class SkillItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int Level { get; set; }
}

public class CreateSkillRequest
{
    [Required]
    public string Category { get; set; } = string.Empty;
    public int Order { get; set; }
    public List<SkillItemDto> Skills { get; set; } = new();
}

public class UpdateSkillRequest
{
    [Required]
    public string Category { get; set; } = string.Empty;
    public int Order { get; set; }
    public List<SkillItemDto> Skills { get; set; } = new();
}
