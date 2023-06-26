using System;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class ProductViewModel
    {
        //
        [Display(Name = "商品ID")]
        [MaxLength(200)]
        public string ProductId { get; set; }
        //
        [Required(ErrorMessage = "{0}不可為空!")]
        [Display(Name = "書名")]
        [MaxLength(200)]
        public string Title { get; set; }
        //
        [Required(ErrorMessage = "{0}不可為空!")]
        [Display(Name = "ISBN")]
        [MaxLength(200)]
        public string ISBN { get; set; }
        //
        [Required(ErrorMessage = "{0}不可為空!")]
        [Display(Name = "作者")]
        [MaxLength(200)]
        public string Author { get; set; }
        //
        [Required(ErrorMessage = "{0}不可為空!")]
        [Display(Name = "出版社")]
        [MaxLength(200)]
        public string Publisher { get; set; }
        //
        [Required(ErrorMessage = "{0}不可為空!")]
        [Display(Name = "出版日期")]
        [MaxLength(200)]
        public string PublicationDate { get; set; }
        //
        [Required(ErrorMessage = "{0}不可為空!")]
        [Display(Name = "新舊程度")]
        [MaxLength(200)]
        public string Degree { get; set; }
        //
        [Required(ErrorMessage = "{0}不可為空!")]
        [Display(Name = "內文")]
        [MaxLength(3000)]
        public string ContentText { get; set; }
        //
        [Display(Name = "圖片封面")]
        [MaxLength(200)]
        public string Image1 { get; set; }
        //
        [Display(Name = "圖片2")]
        [MaxLength(200)]
        public string Image2 { get; set; }
        //
        [Display(Name = "圖片3")]
        [MaxLength(200)]
        public string Image3 { get; set; }
        //
        [Display(Name = "圖片4")]
        [MaxLength(200)]
        public string Image4 { get; set; }
        //
        [Display(Name = "圖片5")]
        [MaxLength(200)]
        public string Image5 { get; set; }
        //
        [Display(Name = "圖片6")]
        [MaxLength(200)]
        public string Image6 { get; set; }
        //
        [Display(Name = "圖片7")]
        [MaxLength(200)]
        public string Image7 { get; set; }
        //
        [Display(Name = "圖片8")]
        [MaxLength(200)]
        public string Image8 { get; set; }
        //
        [Display(Name = "圖片9")]
        [MaxLength(200)]
        public string Image9 { get; set; }
        //
        [Display(Name = "上架狀態")]
        [MaxLength(200)]
        public string Status { get; set; }
        //
        [Required(ErrorMessage = "{0}不可為空!")]
        [Display(Name = "交易方式")]
        [MaxLength(200)]
        public string Trade { get; set; }
        //
        [Required(ErrorMessage = "{0}不可為空!")]
        [Display(Name = "價格")]
        public decimal UnitPrice { get; set; }
        //
        [Display(Name = "新增日期")]
        public DateTime CreateDate { get; set; }
        //
        [Display(Name = "最後編輯日期")]
        public DateTime EditDate { get; set; }
        //
        [MaxLength(200)]
        public string CreateBy { get; set; }
    }
}
