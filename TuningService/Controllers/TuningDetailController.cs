using Microsoft.AspNetCore.Mvc;
using TuningService.DTOs;
using TuningService.Services;

namespace TuningService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TuningDetailController : Controller
    {
        private readonly TuningDetailService _tuningDetailService;

        public TuningDetailController(TuningDetailService tuningDetailService)
        {
            _tuningDetailService = tuningDetailService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTuningDetail([FromBody] TuningDetailDto tuningDetailDto)
        {
            var addedTuningDetail = await _tuningDetailService.AddTuningDetailAsync(tuningDetailDto);
            return Ok(addedTuningDetail);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTuningDetailById(Guid id)
        {
            var tuningDetail = await _tuningDetailService.GetTuningDetailByIdAsync(id);
            if (tuningDetail == null)
            {
                return NotFound();
            }
            return Ok(tuningDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTuningDetail(Guid id, [FromBody] TuningDetailDto tuningDetailDto)
        {
            if (id != tuningDetailDto.Id)
            {
                return BadRequest();
            }

            await _tuningDetailService.UpdateTuningDetailAsync(tuningDetailDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTuningDetail(Guid id)
        {
            await _tuningDetailService.DeleteTuningDetailAsync(id);
            return NoContent();
        }
    }
}
