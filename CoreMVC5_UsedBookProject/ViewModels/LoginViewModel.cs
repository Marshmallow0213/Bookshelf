using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "使用者名稱")]
        [Required(ErrorMessage = "{0}不可為空!")]
        [MaxLength(24, ErrorMessage = "{0}至多24個字。")]
        public string UserName { get; set; }
        [Display(Name = "密碼")]
        [Required(ErrorMessage = "{0}不可為空!")]
        [DataType(DataType.Password)]
        [MaxLength(24, ErrorMessage = "{0}至多24個字。")]
        public string Password { get; set; }
    }
}
