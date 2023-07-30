using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMVC5_UsedBookProject.Models
{
    public class BarterOrder
    {
        public string OrderId { get; set; }
        [Required]
        public string SellerId { get; set; }
        [Required]
        public string BuyerId { get; set; }
        [Required]
        public string DenyReason { get; set; }
        [Required]
        public string SellerProductId { get; set; }
        [Required]
        public string BuyerProductId { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Trade { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public Product Product { get; set; }
    }
}
