namespace Trendify.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public IList<string> Roles { get; internal set; }
    }
}
