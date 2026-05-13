using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Web.DTOs;
using Portfolio.Web.Services;

namespace Portfolio.Web.Pages;

public class ProjectsModel : PageModel
{
    private readonly IProjectService _projectService;

    public List<ProjectDto> Projects { get; set; } = new();

    public ProjectsModel(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task OnGetAsync()
    {
        var allProjects = await _projectService.GetAllAsync();
        Projects = allProjects.Where(p => p.IsPublished).ToList();
    }
}
