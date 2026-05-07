using System.Text.Json.Serialization;

namespace Portfolio.Web.Models;

public class AboutStat
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;

    public int AboutId { get; set; }
    
    [JsonIgnore]
    public About? About { get; set; }
}
