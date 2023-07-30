using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class TextboxViewModel
    {
        public string Id { get; set; }
        [Required]
        public string TextValue { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public IFormFile File1 { get; set; }
        public IFormFile File2 { get; set; }
        public IFormFile File3 { get; set; }
    }
}
