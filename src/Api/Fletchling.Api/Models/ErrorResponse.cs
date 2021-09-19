using Newtonsoft.Json;

namespace Fletchling.Api.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
