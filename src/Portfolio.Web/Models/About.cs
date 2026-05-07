namespace Portfolio.Web.Models;

public class About
{
    public int Id { get; set; }
    public string Bio { get; set; } = string.Empty;
    public string ProfileImage { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<AboutStat> Stats { get; set; } = new List<AboutStat>();
}
