using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class ShoppingcartViewModel
    {
        public int ShoppingcartId { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public string Id { get; set; }
    }
}
