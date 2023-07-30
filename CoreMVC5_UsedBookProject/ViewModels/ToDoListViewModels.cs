using CoreMVC5_UsedBookProject.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class ToDoListViewModels
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public string Date { get; set; }
        [Display(Name = "主標題")]
        [Required]
        public string Maintitle { get; set; }
        [Display(Name = "細項描述")]
        [Required]
        public string Subtitle { get; set; }
        [Display(Name = "權限")]
        public string RoleName { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }

        public DbSet<AdminlistRole> AdminlistRoles { get; set; }
    }
}
