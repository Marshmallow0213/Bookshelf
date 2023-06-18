using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.Collections.Generic;

namespace CoreMVC5_UsedBookProject.Models
{
    public class OrderByBarter
    {
        [Required]
        public string OrderByBarterId { get; set; }
        [Required]
        public string SellerId { get; set; }
        [Required]
        public string BuyerId { get; set; }
        [Required]
        public string DenyReason { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public Product Product { get; set; }
    }
}
