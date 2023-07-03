using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMVC5_UsedBookProject.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string PublicationDate { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string ContentText { get; set; }
        [Required]
        public string Image1 { get; set; }
        [Required]
        public string Image2 { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Trade { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime EditDate { get; set; }
        [Required]
        public string TradingPlaceAndTime { get; set; }
        [Required]
        public string CreateBy { get; set; }
        public User User { get; set; }
    }
}
