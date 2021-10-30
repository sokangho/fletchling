namespace Fletchling.Domain.ApiModels.Requests
{
    public class AddUserRequest
    {
        public string UID { get; set; }
        public long TwitterUserId { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
    }
}