using Microsoft.EntityFrameworkCore;
using TuningService.Data;
using TuningService.DTOs;

namespace TuningService.Services
{
    public class CarService
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CarDto> AddCarAsync(CarDto carDto)
        {
            var car = new Car
            {
                Id = Guid.NewGuid(),
                BrandName = carDto.BrandName,
                Model = carDto.Model,
                MadeYear = carDto.MadeYear,
                UserId = carDto.UserId
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            carDto.Id = car.Id;
            return carDto;
        }

        public async Task<CarDto> GetCarByIdAsync(Guid id)
        {
            var car = await _context.Cars.Include(c => c.TuningDetails).FirstOrDefaultAsync(c => c.Id == id);
            if (car == null)
            {
                return null;
            }

            var carDto = new CarDto
            {
                Id = car.Id,
                BrandName = car.BrandName,
                Model = car.Model,
                MadeYear = car.MadeYear,
                UserId = car.UserId,
                TuningDetails = car.TuningDetails.Select(td => new TuningDetailDto
                {
                    Id = td.Id,
                    TuningPartOfCar = td.TuningPartOfCar,
                    Description = td.Description,
                    CarId = td.CarId
                }).ToList()
            };

            return carDto;
        }

        public async Task<List<CarDto>> GetAllCarsAsync()
        {
            return await _context.Cars.Include(c => c.TuningDetails)
                .Select(car => new CarDto
                {
                    Id = car.Id,
                    BrandName = car.BrandName,
                    Model = car.Model,
                    MadeYear = car.MadeYear,
                    UserId = car.UserId,
                    TuningDetails = car.TuningDetails.Select(td => new TuningDetailDto
                    {
                        Id = td.Id,
                        TuningPartOfCar = td.TuningPartOfCar,
                        Description = td.Description,
                        CarId = td.CarId
                    }).ToList()
                }).ToListAsync();
        }

        public async Task UpdateCarAsync(CarDto carDto)
        {
            var car = await _context.Cars.Include(c => c.TuningDetails).FirstOrDefaultAsync(c => c.Id == carDto.Id);
            if (car == null)
            {
                throw new Exception("Car not found");
            }

            car.BrandName = carDto.BrandName;
            car.Model = carDto.Model;
            car.MadeYear = carDto.MadeYear;
            car.UserId = carDto.UserId;

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}
