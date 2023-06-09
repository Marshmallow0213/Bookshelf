using System.Collections.Generic;

namespace CoreMVC5_UsedBookProject.Models
{
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }

        // Navigation property for UserRoles
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
