namespace TuningService.Data
{
    public class TuningDetail
    {
        public Guid Id { get; set; }
        public string TuningPartOfCar { get; set; }
        public string Description { get; set; }
        public Guid CarId { get; set; }
        public Car Car { get; set; }
    }
}
