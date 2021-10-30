namespace Fletchling.Domain.ApiModels
{
    public class TwitterUser
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public bool Verified { get; set; }
        public string ProfileUrl => $"https://twitter.com/{Username}";
        public string ProfileImageUrl { get; set; }
    }
}