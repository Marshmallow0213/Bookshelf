using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class OrderViewModel
    {
        [Display(Name = "訂單Id")]
        public string OrderId { get; set; }
        [Display(Name = "價格")]
        public decimal SellerUnitPrice { get; set; }
        //
        [Display(Name = "賣家Id")]
        public string SellerId { get; set; }
        [Display(Name = "買家Id")]
        public string BuyerId { get; set; }
        //
        [Display(Name = "賣家Name")]
        public string SellerName { get; set; }
        [Display(Name = "買家Name")]
        public string BuyerName { get; set; }
        [Display(Name = "取消原因")]
        [Required(ErrorMessage = "{0}不可為空!")]
        [MaxLength(200)]
        public string DenyReason { get; set; }
        [Display(Name = "訂單狀態")]
        public string Status { get; set; }
        [Display(Name = "交易方式")]
        public string Trade { get; set; }
        [Display(Name = "商品Id")]
        public string SellerProductId { get; set; }
        [Display(Name = "書名")]
        public string SellerTitle { get; set; }
        [Display(Name = "ISBN")]
        public string SellerISBN { get; set; }
        [Display(Name = "作者")]
        public string SellerAuthor { get; set; }
    }
}
