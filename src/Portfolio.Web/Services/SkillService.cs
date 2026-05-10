using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.DTOs;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public interface ISkillService
{
    Task<List<SkillDto>> GetAllAsync();
    Task<SkillDto?> GetByIdAsync(int id);
    Task<SkillDto> CreateAsync(CreateSkillRequest request);
    Task<SkillDto?> UpdateAsync(int id, UpdateSkillRequest request);
    Task<bool> DeleteAsync(int id);
}

public class SkillService : ISkillService
{
    private readonly AppDbContext _context;

    public SkillService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SkillDto>> GetAllAsync()
    {
        var skills = await _context.Skills
            .Include(s => s.Skills)
            .OrderBy(s => s.Order)
            .ToListAsync();

        return skills.Select(MapToDto).ToList();
    }

    public async Task<SkillDto?> GetByIdAsync(int id)
    {
        var skill = await _context.Skills
            .Include(s => s.Skills)
            .FirstOrDefaultAsync(s => s.Id == id);

        return skill != null ? MapToDto(skill) : null;
    }

    public async Task<SkillDto> CreateAsync(CreateSkillRequest request)
    {
        var skill = new Skill
        {
            Category = request.Category,
            Order = request.Order,
            Skills = request.Skills.Select(si => new SkillItem
            {
                Name = si.Name,
                Icon = si.Icon,
                Level = si.Level
            }).ToList()
        };

        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();

        return MapToDto(skill);
    }

    public async Task<SkillDto?> UpdateAsync(int id, UpdateSkillRequest request)
    {
        var skill = await _context.Skills
            .Include(s => s.Skills)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (skill == null) return null;

        skill.Category = request.Category;
        skill.Order = request.Order;

        // Sync SkillItems
        skill.Skills.Clear();
        foreach (var si in request.Skills)
        {
            skill.Skills.Add(new SkillItem
            {
                Name = si.Name,
                Icon = si.Icon,
                Level = si.Level
            });
        }

        await _context.SaveChangesAsync();
        return MapToDto(skill);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var skill = await _context.Skills.FindAsync(id);
        if (skill == null) return false;

        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();
        return true;
    }

    private static SkillDto MapToDto(Skill skill)
    {
        return new SkillDto
        {
            Id = skill.Id,
            Category = skill.Category,
            Order = skill.Order,
            Skills = skill.Skills.Select(si => new SkillItemDto
            {
                Id = si.Id,
                Name = si.Name,
                Icon = si.Icon,
                Level = si.Level
            }).ToList()
        };
    }
}
