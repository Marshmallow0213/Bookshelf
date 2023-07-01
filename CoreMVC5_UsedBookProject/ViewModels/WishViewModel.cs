using CoreMVC5_UsedBookProject.Models;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class WishViewModel
    {

        public int WishId { get; set; }
        [Required]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ISBN { get; set; }

       
    }
}
