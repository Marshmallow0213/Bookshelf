﻿using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "使用者名稱")]
        [Required(ErrorMessage = "{0}不可為空!")]
        [MaxLength(50, ErrorMessage = "{0}至多50個字。")]
        public string UserName { get; set; }
        [Display(Name = "密碼")]
        [Required(ErrorMessage = "{0}不可為空!")]
        [DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "{0}至多50個字。")]
        [MinLength(8, ErrorMessage = "{0}至少8個字。")]
        public string Password { get; set; }
        [Display(Name = "確認密碼")]
        [Required(ErrorMessage = "{0}不可為空!")]
        [DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "{0}至多50個字。")]
        [Compare(nameof(Password), ErrorMessage = "{0}與{1}不相符。")]
        public string ConfirmPassword { get; set; }
    }
}
