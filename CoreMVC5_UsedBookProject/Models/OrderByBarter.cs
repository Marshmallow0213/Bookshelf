using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.Collections.Generic;

namespace CoreMVC5_UsedBookProject.Models
{
    public class OrderByBarter
    {
        //
        [Required]
        [MaxLength(200)]
        public string OrderByBarterId { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string SellerId { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string BuyerId { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string DenyReason { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string ProductId { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string Status { get; set; }
        //
        [Required]
        public DateTime CreateDate { get; set; }
        //
        public Product Product { get; set; }
    }
}
