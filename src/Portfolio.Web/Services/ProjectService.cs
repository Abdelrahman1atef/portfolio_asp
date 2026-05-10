using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.DTOs;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public interface IProjectService
{
    Task<List<ProjectDto>> GetAllAsync();
    Task<ProjectDto?> GetByIdAsync(int id);
    Task<ProjectDto?> GetBySlugAsync(string slug);
    Task<ProjectDto> CreateAsync(CreateProjectRequest request);
    Task<ProjectDto?> UpdateAsync(int id, UpdateProjectRequest request);
    Task<bool> DeleteAsync(int id);
}

public class ProjectService : IProjectService
{
    private readonly AppDbContext _context;

    public ProjectService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectDto>> GetAllAsync()
    {
        var projects = await _context.Projects
            .OrderBy(p => p.Order)
            .ThenByDescending(p => p.CreatedAt)
            .ToListAsync();

        return projects.Select(MapToDto).ToList();
    }

    public async Task<ProjectDto?> GetByIdAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        return project != null ? MapToDto(project) : null;
    }

    public async Task<ProjectDto?> GetBySlugAsync(string slug)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Slug == slug && p.IsPublished);
        return project != null ? MapToDto(project) : null;
    }

    public async Task<ProjectDto> CreateAsync(CreateProjectRequest request)
    {
        var project = new Project
        {
            Slug = request.Slug,
            Title = request.Title,
            ShortDescription = request.ShortDescription,
            Description = request.Description,
            Features = request.Features,
            TechStack = request.TechStack,
            Image = request.Image,
            LiveUrl = request.LiveUrl,
            GithubUrl = request.GithubUrl,
            Problem = request.Problem,
            Solution = request.Solution,
            Architecture = request.Architecture,
            Challenges = request.Challenges,
            Category = request.Category,
            Order = request.Order,
            IsPublished = request.IsPublished
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return MapToDto(project);
    }

    public async Task<ProjectDto?> UpdateAsync(int id, UpdateProjectRequest request)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return null;

        project.Slug = request.Slug;
        project.Title = request.Title;
        project.ShortDescription = request.ShortDescription;
        project.Description = request.Description;
        project.Features = request.Features;
        project.TechStack = request.TechStack;
        project.Image = request.Image;
        project.LiveUrl = request.LiveUrl;
        project.GithubUrl = request.GithubUrl;
        project.Problem = request.Problem;
        project.Solution = request.Solution;
        project.Architecture = request.Architecture;
        project.Challenges = request.Challenges;
        project.Category = request.Category;
        project.Order = request.Order;
        project.IsPublished = request.IsPublished;

        await _context.SaveChangesAsync();
        return MapToDto(project);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return true;
    }

    private static ProjectDto MapToDto(Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Slug = project.Slug,
            Title = project.Title,
            ShortDescription = project.ShortDescription,
            Description = project.Description,
            Features = project.Features,
            TechStack = project.TechStack,
            Image = project.Image,
            LiveUrl = project.LiveUrl,
            GithubUrl = project.GithubUrl,
            Problem = project.Problem,
            Solution = project.Solution,
            Architecture = project.Architecture,
            Challenges = project.Challenges,
            Category = project.Category,
            Order = project.Order,
            IsPublished = project.IsPublished,
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt
        };
    }
}
