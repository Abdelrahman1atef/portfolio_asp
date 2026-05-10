using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.DTOs;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class SkillsController : ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillsController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSkills()
    {
        var skills = await _skillService.GetAllAsync();
        return Ok(skills);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSkill(int id)
    {
        var skill = await _skillService.GetByIdAsync(id);
        if (skill == null) return NotFound(new { message = "Skill not found" });
        return Ok(skill);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateSkill([FromBody] CreateSkillRequest request)
    {
        var result = await _skillService.CreateAsync(request);
        return CreatedAtAction(nameof(GetSkill), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateSkill(int id, [FromBody] UpdateSkillRequest request)
    {
        var result = await _skillService.UpdateAsync(id, request);
        if (result == null) return NotFound(new { message = "Skill not found" });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteSkill(int id)
    {
        var deleted = await _skillService.DeleteAsync(id);
        if (!deleted) return NotFound(new { message = "Skill not found" });
        return Ok(new { message = "Skill category deleted" });
    }
}
