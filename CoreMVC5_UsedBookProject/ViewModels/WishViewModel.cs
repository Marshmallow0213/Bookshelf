using CoreMVC5_UsedBookProject.Models;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class WishViewModel
    {
        public int WishId { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string UserName { get; set; }
    }
}
