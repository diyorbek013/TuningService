namespace TuningService.Models
{
    public class ApiBaseResultModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ApiBaseResultModel()
        {
            Success = true;
        }

        public ApiBaseResultModel(string errorMessage)
        {
            Success = false;
            Message = errorMessage;
        }
    }
}
