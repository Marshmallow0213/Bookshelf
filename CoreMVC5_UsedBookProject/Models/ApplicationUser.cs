namespace CoreMVC5_UsedBookProject.Models
{
    public class ApplicationUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string PhoneNo { get; set; }
        public string Role { get; set; }
        public string[] Roles { get; set; }
        public string UserIcon { get; set; }
    }
}
