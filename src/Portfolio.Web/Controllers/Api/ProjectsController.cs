using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.DTOs;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        var projects = await _projectService.GetAllAsync();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        var project = await _projectService.GetByIdAsync(id);
        if (project == null) return NotFound(new { message = "Project not found" });
        return Ok(project);
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetProjectBySlug(string slug)
    {
        var project = await _projectService.GetBySlugAsync(slug);
        if (project == null) return NotFound(new { message = "Project not found" });
        return Ok(project);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
    {
        var result = await _projectService.CreateAsync(request);
        return CreatedAtAction(nameof(GetProject), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectRequest request)
    {
        var result = await _projectService.UpdateAsync(id, request);
        if (result == null) return NotFound(new { message = "Project not found" });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var deleted = await _projectService.DeleteAsync(id);
        if (!deleted) return NotFound(new { message = "Project not found" });
        return Ok(new { message = "Project deleted" });
    }
}
