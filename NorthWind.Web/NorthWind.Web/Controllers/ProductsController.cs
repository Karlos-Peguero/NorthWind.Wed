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
        public IActionResult Edit(int id)
        {
            Product product = GetProductById(id);

            return View(new ProductsEditViewModel()
            {
                ProductID = product.ProductId,
                ProductName = product.ProductName,
                CategoryID = product.CategoryId ?? 0,
                SupplierID = product.SupplierId ?? 0,
                UnitPrice = product.UnitPrice,
                Categories = _northwindContext.Categories.ToList(),
                Suppliers = _northwindContext.Suppliers.ToList()
            });
        }
        [HttpPost]
        public IActionResult Edit(int id, ProductsEditViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)

                {
                    Product product = GetProductById(id);
                    product.ProductId = model.ProductID;
                    product.ProductName = model.ProductName;
                    product.CategoryId = model.CategoryID;
                    product.SupplierId = model.SupplierID;
                    product.UnitPrice = model.UnitPrice;
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
        public IActionResult Detail(int id)
        {
            Product? product = GetProductById(id);
            return View(product);
        }

        private Product GetProductById(int id)
        {
            return _northwindContext.Products
                            .Include(p => p.Category)
                            .Include(p => p.Supplier)
                            .First(p => p.ProductId == id);
        }

        public IActionResult Delete(int id)
        {
            return View(GetProductById(id));
        }
        [HttpPost]
        public IActionResult Delete(int id, IFormCollection form)
        {
            try
            {
                Product product = GetProductById(id);
                _northwindContext.Products.Remove(product);
                _northwindContext.SaveChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }
    }

}
