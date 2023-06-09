using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.Models
{
    public class User
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Nickname { get; set; }

        public string Email { get; set; }
        public string PhoneNo { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
