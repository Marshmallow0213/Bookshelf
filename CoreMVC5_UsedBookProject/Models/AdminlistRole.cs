using CoreMVC5_UsedBookProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CoreMVC5_UsedBookProject.Models
{
    public class AdminlistRole
    {
        public string ListId { get; set; }

        public Adminlist Adminlist { get; set; }

        public string RoleId { get; set; }
        public AdminRole Role { get; set; }
    }
}
