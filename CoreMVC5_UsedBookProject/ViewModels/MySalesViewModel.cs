using System.Collections.Generic;

namespace CoreMVC5_UsedBookProject.ViewModels
{
    public class MySalesViewModel
    {
        public OrderViewModel Order { get; set; }
        public List<OrderViewModel> Orders { get; set; }
        public Dictionary<string, int> OrdersCount { get; set; }
        public int[] PagesCount { get; set; }
        public string StatusPage { get; set; }
    }
}
