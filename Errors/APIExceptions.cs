using API.Errors;

namespace SKINET_Ecommerce.Errors
{
    public class APIExceptions : APIResponse
    {
        public APIExceptions(int statusCode, string message = null) : base(statusCode, message)
        { 

        }

        public string Details {  get; set; }

    }
}
