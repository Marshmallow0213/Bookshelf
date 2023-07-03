using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC5_UsedBookProject.ViewModels
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
