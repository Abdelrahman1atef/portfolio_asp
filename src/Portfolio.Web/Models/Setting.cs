namespace Portfolio.Web.Models;

public class Setting
{
    public int Id { get; set; }
    public string Name { get; set; } = "Abdelrahman Atef";
    public string Email { get; set; } = "Abdelrahmanatef3221@gmail.com";
    public string Phone { get; set; } = "+20 102 232 2742";
    
    // Social Links flattened
    public string GithubUrl { get; set; } = "https://github.com/Abdelrahman1atef";
    public string LinkedinUrl { get; set; } = "https://linkedin.com/in/abdelrahman-atef-b1a59b24a";
    public string WhatsappUrl { get; set; } = "https://wa.me/201022322742";
    
    public string CvFile { get; set; } = string.Empty;
    public string ThemePreference { get; set; } = "dark";
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
