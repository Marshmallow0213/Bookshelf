using System.Collections.Generic;

namespace CoreMVC5_UsedBookProject.Models
{
    public class AdministratorUser
{
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        public ICollection<AdministratorUserRoles> AdministratorRoles { get; set; }
    }
}