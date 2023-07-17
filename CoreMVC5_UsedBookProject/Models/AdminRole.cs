using CoreMVC5_UsedBookProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CoreMVC5_UsedBookProject.Models
{
    public class AdminRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
