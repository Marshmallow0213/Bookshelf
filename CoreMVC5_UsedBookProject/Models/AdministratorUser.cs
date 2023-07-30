using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.Models
{
    public class AdministratorUser
{
        [Display(Name = "編號")]
        public string Id { get; set; }
        [Display(Name = "姓名")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "密碼")]
        [Required]
        public string Password { get; set; }
        [Display(Name = "綽號")]
        [Required]
        public string Nickname { get; set; }
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "電話")]
        [Required]
        public string PhoneNo { get; set; }

        public ICollection<AdministratorUserRoles> AdministratorRoles { get; set; }
    }
}