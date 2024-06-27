using TuningService.Data;
using TuningService.DTOs;
using File = TuningService.Data.File;

namespace TuningService.Services
{
    public class FileUploadService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public FileUploadService(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task SaveFileAsync(FileUploadDto fileUploadDto)
        {
            var fileName = Guid.NewGuid().ToString();
            var uploadsFolderPath = Path.Combine(_environment.ContentRootPath, "Files");

            // Ensure the directory exists
            Directory.CreateDirectory(uploadsFolderPath);

            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileUploadDto.File.CopyToAsync(stream);
            }

            var fileEntity = new File
            {
                Id = Guid.NewGuid(),
                UserId = fileUploadDto.UserId,
                CreatedTime = DateTime.UtcNow,
                FilePath = filePath
            };

            _context.Files.Add(fileEntity);
            await _context.SaveChangesAsync();
        }
    }
}
