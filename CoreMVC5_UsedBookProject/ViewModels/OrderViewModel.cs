using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class OrderViewModel
    {
        [Display(Name = "訂單Id")]
        public string OrderId { get; set; }
        [Display(Name = "價格")]
        public decimal UnitPrice { get; set; }
        //
        [Display(Name = "賣家Id")]
        public string SellerId { get; set; }
        [Display(Name = "買家Id")]
        public string BuyerId { get; set; }
        [Display(Name = "取消原因")]
        public string DenyReason { get; set; }
        [Display(Name = "訂單狀態")]
        public string Status { get; set; }
        [Display(Name = "商品ID")]
        public string ProductId { get; set; }
        [Display(Name = "書名")]
        public string Title { get; set; }
        [Display(Name = "圖片封面")]
        public string Image1 { get; set; }
    }
}
