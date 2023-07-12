using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class MixUserViewModel
    {
        [Display(Name = "編號")]
        public string UserID { get; set; }
        [Display(Name = "姓名")]
        public string UserName { get; set; }

        [Display(Name = "權限")]
        public string RoleName { get; set; }
    }
}
