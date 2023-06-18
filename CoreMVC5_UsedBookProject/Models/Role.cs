using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.Models
{
    public class Role
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        // Navigation property for UserRoles
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
