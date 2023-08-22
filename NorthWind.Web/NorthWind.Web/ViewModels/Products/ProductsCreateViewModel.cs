using NorthWind.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace NorthWind.Web.ViewModels.Products
{
    public class ProductsCreateViewModel
    {
        [Required]
        [MinLength(3)]
        public string ProductName { get; set; }
        public int CategoryID { get; set; }

        public int SupplierID { get; set; }

        public decimal? UnitPrice { get; set; }

        public string ErrorMessage { get; internal set; }

        public List<Category>? Categories { get; internal set; }
        public List<Supplier>? Suppliers { get; internal set; }
    }

    public class ProductsEditViewModel
    {
        public int ProductID { get; set; }

        [Required]
        [MinLength(3)]
        public string ProductName { get; set; }
        public int CategoryID { get; set; }

        public int SupplierID { get; set; }

        public decimal? UnitPrice { get; set; }

        public string ErrorMessage { get; internal set; }

        public List<Category>? Categories { get; internal set; }
        public List<Supplier>? Suppliers { get; internal set; }
    }
}
