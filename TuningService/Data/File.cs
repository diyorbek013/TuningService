namespace TuningService.Data
{
    public class File
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string FilePath { get; set; }
    }
}
