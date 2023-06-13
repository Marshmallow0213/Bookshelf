using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.Models
{
    public class ChangeOrder
    {
        [Key]
        public string ChangeOrderId { get; set; }
        [Required]
        public string ProductId { get; set; }
      
        [Required]

        public string Buyer { get; set; }
        [Required]
        public string Seller { get; set; }
        [Required]


        public string denyreason { get; set; }
    }
}
