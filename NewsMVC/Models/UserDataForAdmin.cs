namespace NewsMVC.Models
{
#nullable disable
    public class UserDataForAdmin
    {
        public string UserName { get; set; }

        public string Email { get; set; }
        public string[] Roles { get; set; }

        public DateTimeOffset? BanEnds { get; set; }
    }
}
