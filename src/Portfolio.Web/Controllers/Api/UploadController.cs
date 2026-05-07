using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Services;

namespace Portfolio.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly ICloudinaryService _cloudinaryService;

    public UploadController(ICloudinaryService cloudinaryService)
    {
        _cloudinaryService = cloudinaryService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UploadFile(IFormFile file, [FromForm] string folder = "general")
    {
        if (file == null || file.Length == 0)
            return BadRequest(new { message = "No file uploaded" });

        try
        {
            using var stream = file.OpenReadStream();
            var url = await _cloudinaryService.UploadAsync(stream, file.FileName, folder);

            return Ok(new { url });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Upload failed", error = ex.Message });
        }
    }
}
