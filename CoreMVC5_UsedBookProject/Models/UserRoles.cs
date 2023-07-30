using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.Models
{
    public class UserRoles
    {
        public string UserId { get; set; }

        public User User { get; set; } //Navigation property for Users

        public string RoleId { get; set; }
        public Role Role { get; set; } //Navigation property for Roles
    }
}
