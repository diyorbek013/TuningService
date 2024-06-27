namespace TuningService.DTOs
{
    public class TuningDetailDto
    {
        public Guid Id { get; set; }
        public string TuningPartOfCar { get; set; }
        public string Description { get; set; }
        public Guid CarId { get; set; }
    }
}
