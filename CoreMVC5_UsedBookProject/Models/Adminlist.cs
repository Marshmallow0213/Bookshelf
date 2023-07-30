using CoreMVC5_UsedBookProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CoreMVC5_UsedBookProject.Models
{
    public class Adminlist
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "日期")]
        public string Date { get; set; }
        [Display(Name = "主標題")]
        [Required]
        public string Maintitle { get; set; }
        [Display(Name = "細項描述")]
        [Required]
        public string Subtitle { get; set; }

        public ICollection<AdminlistRole> AdminlistRoles { get; set; }
    }
}
