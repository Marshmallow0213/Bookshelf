using CoreMVC5_UsedBookProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class AdministratorUserHomePage
    {
        [Display(Name = "編號")]
        public string Id { get; set; }
        [Display(Name = "姓名")]
       
        
        public string Name { get; set; }
        [Display(Name = "密碼")]
    
        public string Password { get; set; }
        [Display(Name = "綽號")]
        [Required]
        public string Nickname { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "請輸入Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "電話")]
        [Required]
        [Phone(ErrorMessage = "09xx-xxx-xxx")]
        public string PhoneNo { get; set; }

        public ICollection<AdministratorUserRoles> AdministratorRoles { get; set; }
    }
}
