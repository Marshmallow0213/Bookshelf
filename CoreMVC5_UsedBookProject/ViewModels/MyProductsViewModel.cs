using System.Collections.Generic;
using CoreMVC5_UsedBookProject.Models;

namespace CoreMVC5_UsedBookProject.ViewModel
{
    public class MyProductsViewModel
    {
        public ProductViewModel Product { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public Dictionary<string, int> ProductsCount { get; set; }
        public int[] PagesCount { get; set; }
        public string StatusPage { get; set; }
        public int[] ProductNewLimit { get; set; }
    }
}
