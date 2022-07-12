namespace TodoAPI.Errors
{
    public class ErrorResult
    {
        public ErrorResult(int statusCode, string? message = null, string? details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; }
        public string? Message { get; }
        public string? Details { get; }
    }
}
