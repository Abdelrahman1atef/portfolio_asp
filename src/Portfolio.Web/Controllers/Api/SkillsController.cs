using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.Models;

namespace Portfolio.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class SkillsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SkillsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetSkills()
    {
        var skills = await _context.Skills.Include(s => s.Skills).OrderBy(s => s.Order).ToListAsync();
        return Ok(skills);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateSkill([FromBody] Skill skill)
    {
        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSkills), new { id = skill.Id }, skill); // Note: Should ideally be GetSkill
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateSkill(int id, [FromBody] Skill skillUpdate)
    {
        var skill = await _context.Skills.Include(s => s.Skills).FirstOrDefaultAsync(s => s.Id == id);
        if (skill == null) return NotFound(new { message = "Skill not found" });

        skill.Category = skillUpdate.Category;
        skill.Order = skillUpdate.Order;
        
        // simple replace for child items
        _context.SkillItems.RemoveRange(skill.Skills);
        skill.Skills = skillUpdate.Skills;

        await _context.SaveChangesAsync();
        return Ok(skill);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteSkill(int id)
    {
        var skill = await _context.Skills.FindAsync(id);
        if (skill == null) return NotFound(new { message = "Skill not found" });

        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Skill category deleted" });
    }
}
