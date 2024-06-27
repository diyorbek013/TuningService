using Microsoft.AspNetCore.Mvc;
using TuningService.DTOs;
using TuningService.Services;

namespace TuningService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : Controller
    {
        private readonly FileUploadService _fileUploadService;

        public FileUploadController(FileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] FileUploadDto fileUploadDto)
        {
            if (fileUploadDto.File == null || fileUploadDto.File.Length == 0)
            {
                return BadRequest("File is empty");
            }

            await _fileUploadService.SaveFileAsync(fileUploadDto);
            return Ok(new { Message = "File uploaded successfully" });
        }
    }
}
