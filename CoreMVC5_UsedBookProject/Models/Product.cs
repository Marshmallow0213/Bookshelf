using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMVC5_UsedBookProject.Models
{
    public class Product
    {
        //Add product
        [Required]
        [MaxLength(200)]
        public string ProductId { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string ISBN { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string Author { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string Publisher { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string PublicationDate { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string Degree { get; set; }
        //
        [Required]
        [MaxLength(3000)]
        public string ContentText { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string Image1 { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string Image2 { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string Status { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string Trade { get; set; }
        //
        [Required]
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        //
        [Required]
        public DateTime CreateDate { get; set; }
        //
        [Required]
        public DateTime EditDate { get; set; }
        //
        [Required]
        [MaxLength(200)]
        public string CreateBy { get; set; }
    }
}
