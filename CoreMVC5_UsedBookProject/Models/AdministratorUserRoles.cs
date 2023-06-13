using CoreMVC5_UsedBookProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.Models
{
    public class AdministratorUserRoles
    {
        public string UserId { get; set; }

        public AdministratorUser User { get; set; } //Navigation property for Users

        public string RoleId { get; set; }
        public AdministratorRole Role { get; set; } //Navigation property for Roles
    }
}