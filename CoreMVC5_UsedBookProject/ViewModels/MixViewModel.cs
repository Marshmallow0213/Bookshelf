using CoreMVC5_UsedBookProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class MixViewModel
    {
        [Display(Name = "編號")]
        public string UserID { get; set; }
        [Display(Name = "姓名")]
        public string UserName { get; set; }

        [Display(Name = "權限")]
        public string RoleName { get; set; }
    }
}
