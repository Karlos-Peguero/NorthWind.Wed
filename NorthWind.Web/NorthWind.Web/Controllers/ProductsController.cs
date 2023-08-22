using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthWind.Web.Data;
using NorthWind.Web.Models;
using NorthWind.Web.ViewModels.Products;

namespace NorthWind.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly NorthwindContext _northwindContext;

        public ProductsController(NorthwindContext northwindContext)
        {
            this._northwindContext = northwindContext;
        }


        public IActionResult Index()
        {
            var products = _northwindContext.Products.ToList();

            return View(products);
        }

        public IActionResult Create()
        {
            return View(new ProductsCreateViewModel()
            {
                Categories = _northwindContext.Categories.ToList(),
                Suppliers = _northwindContext.Suppliers.ToList()
            });
        }

        [HttpPost]
        public IActionResult Create(ProductsCreateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)

                {
                    var product = new Product();
                    product.ProductName = model.ProductName;
                    product.CategoryId = model.CategoryID;
                    product.SupplierId = model.SupplierID;
                    product.UnitPrice = model.UnitPrice;
                    _northwindContext.Products.Add(product);
                    _northwindContext.SaveChanges();

                    return RedirectToAction("Detail", new
                    {
                        id = product.ProductId
                    });
                }
                else
                {
                    model.ErrorMessage = "Some fields are not valid. Please check the form and try again.";
                }
            }
            catch (Exception e)
            {
                model.ErrorMessage = e.Message;
            }

            model.Categories = _northwindContext.Categories.ToList();
            model.Suppliers = _northwindContext.Suppliers.ToList();

            return View(model);
        }
    }
}
