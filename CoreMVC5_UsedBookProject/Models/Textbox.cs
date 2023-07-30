using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.Models
{
    public class Textbox
    {
        public string Id { get; set; }
        [Required]
        public string TextValue { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
    }
}
