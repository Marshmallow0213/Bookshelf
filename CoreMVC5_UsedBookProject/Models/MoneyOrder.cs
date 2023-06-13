using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.Models
{
    public class MoneyOrder

    {
      

        [Key]
        public string MoneyOrderId { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]

        public string denyreason { get; set; }
        [Required]

        public string Buyer { get; set; }
        [Required]
        public string Seller { get; set; }

        [Required]
        public string ProductName { get; set; }





    }
}
