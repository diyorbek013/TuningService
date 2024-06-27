namespace TuningService.DTOs
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public int MadeYear { get; set; }
        public string UserId { get; set; }
        public List<TuningDetailDto> TuningDetails { get; set; } = new List<TuningDetailDto>();
    }
}
