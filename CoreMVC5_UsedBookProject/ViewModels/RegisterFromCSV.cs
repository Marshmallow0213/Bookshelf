using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class RegisterFromCSV
    {
        [Display(Name = "CSV")]
        [Required(ErrorMessage = "{0}不可為空!")]
        public IFormFile File { get; set; }
    }
}
