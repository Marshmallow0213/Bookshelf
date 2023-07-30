using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CoreMVC5_UsedBookProject.Models
{
    public class announcement
    {
        [Display(Name = "編號")]
        public int Id { get; set; }
        [Display(Name = "公告訊息")]
        public string Message { get; set; }
        [Display(Name = "日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }
    }
}
