using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Web.DTOs;
using Portfolio.Web.Services;

namespace Portfolio.Web.Pages;

public class ProjectModel : PageModel
{
    private readonly IProjectService _projectService;

    public ProjectDto? Project { get; set; }

    public ProjectModel(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        if (string.IsNullOrEmpty(slug)) return RedirectToPage("/Index");

        Project = await _projectService.GetBySlugAsync(slug);

        if (Project == null) return NotFound();

        return Page();
    }
}
