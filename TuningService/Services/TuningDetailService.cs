using TuningService.Data;
using TuningService.DTOs;

namespace TuningService.Services
{
    public class TuningDetailService
    {
        private readonly ApplicationDbContext _context;

        public TuningDetailService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TuningDetailDto> AddTuningDetailAsync(TuningDetailDto tuningDetailDto)
        {
            var tuningDetail = new TuningDetail
            {
                Id = Guid.NewGuid(),
                TuningPartOfCar = tuningDetailDto.TuningPartOfCar,
                Description = tuningDetailDto.Description,
                CarId = tuningDetailDto.CarId
            };

            _context.TuningDetails.Add(tuningDetail);
            await _context.SaveChangesAsync();

            tuningDetailDto.Id = tuningDetail.Id;
            return tuningDetailDto;
        }

        public async Task<TuningDetailDto> GetTuningDetailByIdAsync(Guid id)
        {
            var tuningDetail = await _context.TuningDetails.FindAsync(id);
            if (tuningDetail == null)
            {
                return null;
            }

            return new TuningDetailDto
            {
                Id = tuningDetail.Id,
                TuningPartOfCar = tuningDetail.TuningPartOfCar,
                Description = tuningDetail.Description,
                CarId = tuningDetail.CarId
            };
        }

        public async Task UpdateTuningDetailAsync(TuningDetailDto tuningDetailDto)
        {
            var tuningDetail = await _context.TuningDetails.FindAsync(tuningDetailDto.Id);
            if (tuningDetail == null)
            {
                throw new Exception("Tuning detail not found");
            }

            tuningDetail.TuningPartOfCar = tuningDetailDto.TuningPartOfCar;
            tuningDetail.Description = tuningDetailDto.Description;
            tuningDetail.CarId = tuningDetailDto.CarId;

            _context.TuningDetails.Update(tuningDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTuningDetailAsync(Guid id)
        {
            var tuningDetail = await _context.TuningDetails.FindAsync(id);
            if (tuningDetail != null)
            {
                _context.TuningDetails.Remove(tuningDetail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
