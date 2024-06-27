namespace TuningService.DTOs
{
    public class FileUploadDto
    {
        public IFormFile File { get; set; }
        public string UserId { get; set; }
    }
}
