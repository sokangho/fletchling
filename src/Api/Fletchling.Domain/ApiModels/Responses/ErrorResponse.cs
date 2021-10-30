namespace Fletchling.Domain.ApiModels.Responses
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}