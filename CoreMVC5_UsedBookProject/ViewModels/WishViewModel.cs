using CoreMVC5_UsedBookProject.Models;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class WishViewModel
    {

        public int WishId { get; set; }
        [Required]
        public string Id { get; set; }
        [Required(ErrorMessage = "{0}不可為空!")]
        [Display(Name = "書名")]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required(ErrorMessage = "{0}不可為空!")]
        [Display(Name = "ISBN")]
        [RegularExpression(@"^\d{10}$|^\d{13}$", ErrorMessage = "ISBN必須是10位或13位數字。")]
        public string ISBN { get; set; }

       
    }
}
