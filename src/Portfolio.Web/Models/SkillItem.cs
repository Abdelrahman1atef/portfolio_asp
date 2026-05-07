using System.Text.Json.Serialization;

namespace Portfolio.Web.Models;

public class SkillItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public string Icon { get; set; } = string.Empty;

    public int SkillId { get; set; }
    
    [JsonIgnore]
    public Skill? Skill { get; set; }
}
