namespace TuningService.Settings
{
    public static class ErrorHelper
    {
        public static string GetError(ErrorHelperType errorType, string details)
        {
            switch (errorType)
            {
                case ErrorHelperType.ERROR_INTERNAL:
                    return $"Internal Error: {details}";
                // Add other error types as needed
                default:
                    return $"Unknown Error: {details}";
            }
        }
    }

    public enum ErrorHelperType
    {
        ERROR_INTERNAL
        // Add other error types as needed
    }

}
