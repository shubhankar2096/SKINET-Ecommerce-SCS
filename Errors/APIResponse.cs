
namespace API.Errors
{
    public class APIResponse
    {
        public APIResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch // switch statement with lambda expression
            {
                400 => "A bad request you have made",
                401 => "Azuthorized, you are not",
                404 => "Resource found, you are not",
                500 => "Error at server side",
                _ => null //default
            };
        }
    }
}
