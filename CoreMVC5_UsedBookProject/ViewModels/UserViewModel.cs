using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "使用者Id")]
        public string Id { get; set; }
        [Display(Name = "使用者名稱")]
        public string Name { get; set; }
        [Display(Name = "暱稱")]
        [Required(ErrorMessage = "{0}不可為空!")]
        public string Nickname { get; set; }
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "{0}格式不正確!")]
        public string Email { get; set; }
        [Display(Name = "電話")]
        [Phone(ErrorMessage = "{0}格式不正確!")]
        public string PhoneNo { get; set; }
    }
}
