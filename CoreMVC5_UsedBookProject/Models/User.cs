using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.Models
{
    public class User
    {
        public string Id { get; set; }
        [Display(Name = "姓名")]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name = "綽號")]
        [Required]
        public string Nickname { get; set; }

        public string Email { get; set; }
        [Display(Name = "電話")]
        public string PhoneNo { get; set; }
        [Required]
        public string UserIcon { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
