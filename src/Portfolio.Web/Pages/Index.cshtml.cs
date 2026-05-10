using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Web.DTOs;
using Portfolio.Web.Services;

namespace Portfolio.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IAboutService _aboutService;
    private readonly ISettingService _settingService;
    private readonly IProjectService _projectService;
    private readonly ISkillService _skillService;
    private readonly ITestimonialService _testimonialService;

    public AboutDto? About { get; set; }
    public SettingDto? Settings { get; set; }
    public List<ProjectDto> Projects { get; set; } = new();
    public List<SkillDto> Skills { get; set; } = new();
    public List<TestimonialDto> Testimonials { get; set; } = new();

    public IndexModel(
        IAboutService aboutService,
        ISettingService settingService,
        IProjectService projectService,
        ISkillService skillService,
        ITestimonialService testimonialService)
    {
        _aboutService = aboutService;
        _settingService = settingService;
        _projectService = projectService;
        _skillService = skillService;
        _testimonialService = testimonialService;
    }

    public async Task OnGetAsync()
    {
        About = await _aboutService.GetAboutAsync();
        Settings = await _settingService.GetSettingsAsync();
        
        var allProjects = await _projectService.GetAllAsync();
        Projects = allProjects.Where(p => p.IsPublished).Take(3).ToList();
        
        Skills = await _skillService.GetAllAsync();
        Testimonials = await _testimonialService.GetAllAsync();
    }
}
