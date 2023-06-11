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
        [MaxLength(30, ErrorMessage = "{0}至多30個字。")]
        public string Nickname { get; set; }
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "{0}格式不正確!")]
        [MaxLength(100, ErrorMessage = "{0}至多100個字。")]
        public string Email { get; set; }
        [Display(Name = "電話")]
        [Phone(ErrorMessage = "{0}格式不正確!")]
        [MaxLength(20, ErrorMessage = "{0}至多20個字。")]
        public string PhoneNo { get; set; }
    }
}
