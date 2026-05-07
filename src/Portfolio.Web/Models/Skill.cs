namespace Portfolio.Web.Models;

public class Skill
{
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public int Order { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<SkillItem> Skills { get; set; } = new List<SkillItem>();
}
