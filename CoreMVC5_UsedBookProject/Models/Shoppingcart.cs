using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.Models
{
    public class Shoppingcart
    {
        public int ShoppingcartId { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public string Id { get; set; }
        public Product Product { get; set; } //Navigation property for Roles
        public User User { get; set; } //Navigation property for Roles
    }
}
