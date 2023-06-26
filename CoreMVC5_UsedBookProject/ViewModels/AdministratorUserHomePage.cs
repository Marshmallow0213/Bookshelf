using CoreMVC5_UsedBookProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class AdministratorUserHomePage
    {
        public string Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "最少需要3個字元")]
        public string Name { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        [Required(ErrorMessage = "請輸入Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\09d{2}\-?\d{3}\-?\d{3}$", ErrorMessage = "09xx-xxx-xxx")]
        public string PhoneNo { get; set; }

        public ICollection<AdministratorUserRoles> AdministratorRoles { get; set; }
    }
}
