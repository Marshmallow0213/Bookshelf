using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMVC5_UsedBookProject.Models
{
    public class Order
    {
        [Required]
        public string OrderId { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
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
        public string Trade { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public Product Product { get; set; }
    }
}
