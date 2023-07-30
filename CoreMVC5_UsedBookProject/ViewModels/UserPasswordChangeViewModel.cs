using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class UserPasswordChangeViewModel
    {
        [Display(Name = "舊密碼")]
        [Required(ErrorMessage = "{0}不可為空!")]
        [DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "{0}至多50個字。")]
        [MinLength(8, ErrorMessage = "{0}至少8個字。")]
        public string OldPassword { get; set; }
        [Display(Name = "新密碼")]
        [Required(ErrorMessage = "{0}不可為空!")]
        [DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "{0}至多50個字。")]
        [MinLength(8, ErrorMessage = "{0}至少8個字。")]
        public string Password { get; set; }
        [Display(Name = "確認新密碼")]
        [Required(ErrorMessage = "{0}不可為空!")]
        [DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "{0}至多50個字。")]
        [Compare(nameof(Password), ErrorMessage = "{0}與{1}不相符。")]
        public string ConfirmPassword { get; set; }
    }
}
