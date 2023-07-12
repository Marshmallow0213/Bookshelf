using System.Collections.Generic;

namespace CoreMVC5_UsedBookProject.Models
{
    public class AdministratorRole
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<AdministratorUserRoles> UserRoles { get; set; }
    }
}
