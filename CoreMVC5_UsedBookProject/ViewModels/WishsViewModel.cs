using CoreMVC5_UsedBookProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class WishsViewModel
    {
        public List<WishViewModel> Wishs { get; set; }
        public int WishId { get; set; }
        public string Id { get; set; }
        [Required(ErrorMessage = "{0}不可為空!")]
        [Display(Name = "書名")]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required(ErrorMessage = "{0}不可為空!")]
        [Display(Name = "ISBN")]
        [MaxLength(13, ErrorMessage = "{0}至多13個字。")]
        [MinLength(10, ErrorMessage = "{0}至少13個字。")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "ISBN必須是13位數字。")]
        public string ISBN { get; set; }
        [Display(Name = "許願者")]
        public string UserName { get; set; }
        public List<string> ISBNproducts { get; set; }
        public int[] PagesCount { get; set; }
    }
}
