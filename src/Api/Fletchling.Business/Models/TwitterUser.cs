namespace Fletchling.Business.Models
{
    public class TwitterUser
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public bool Verified { get; set; }
        public string ProfileUrl { get { return $"https://twitter.com/{Username}"; } }
        public string ProfileImageUrl { get; set; }
    }
}
