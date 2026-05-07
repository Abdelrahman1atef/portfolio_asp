using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.Models;

namespace Portfolio.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProjectsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        var projects = await _context.Projects.OrderBy(p => p.Order).ThenByDescending(p => p.CreatedAt).ToListAsync();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound(new { message = "Project not found" });
        return Ok(project);
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetProjectBySlug(string slug)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Slug == slug && p.IsPublished);
        if (project == null) return NotFound(new { message = "Project not found" });
        return Ok(project);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProject([FromBody] Project project)
    {
        if (await _context.Projects.AnyAsync(p => p.Slug == project.Slug))
            return BadRequest(new { message = "A project with this slug already exists" });

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] Project projectUpdate)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound(new { message = "Project not found" });

        // Update fields
        project.Slug = projectUpdate.Slug;
        project.Title = projectUpdate.Title;
        project.ShortDescription = projectUpdate.ShortDescription;
        project.Description = projectUpdate.Description;
        project.Features = projectUpdate.Features;
        project.TechStack = projectUpdate.TechStack;
        project.Image = projectUpdate.Image;
        project.LiveUrl = projectUpdate.LiveUrl;
        project.GithubUrl = projectUpdate.GithubUrl;
        project.Problem = projectUpdate.Problem;
        project.Solution = projectUpdate.Solution;
        project.Architecture = projectUpdate.Architecture;
        project.Challenges = projectUpdate.Challenges;
        project.Category = projectUpdate.Category;
        project.Order = projectUpdate.Order;
        project.IsPublished = projectUpdate.IsPublished;

        await _context.SaveChangesAsync();
        return Ok(project);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound(new { message = "Project not found" });

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Project deleted" });
    }
}
