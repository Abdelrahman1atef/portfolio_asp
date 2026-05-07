using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.Models;

namespace Portfolio.Web.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public About? About { get; set; }
    public Setting? Settings { get; set; }
    public List<Project> Projects { get; set; } = new();
    public List<Skill> Skills { get; set; } = new();
    public List<Testimonial> Testimonials { get; set; } = new();

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        About = await _context.Abouts.Include(a => a.Stats).FirstOrDefaultAsync();
        Settings = await _context.Settings.FirstOrDefaultAsync();
        Projects = await _context.Projects.Where(p => p.IsPublished).OrderBy(p => p.Order).Take(3).ToListAsync();
        Skills = await _context.Skills.Include(s => s.Skills).OrderBy(s => s.Order).ToListAsync();
        Testimonials = await _context.Testimonials.OrderBy(t => t.Order).ToListAsync();
    }
}
