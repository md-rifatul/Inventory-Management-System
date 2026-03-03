using Inventory.Application.Interfaces.IServices;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventory.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        public ProductController(IProductService productService, ICategoryService categoryService, ISupplierService supplierService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }
        public IActionResult Create()
        {
            var categories = _categoryService.GetAllCategories();
            var suppliers = _supplierService.GetAllSuppliers();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.AddProduct(product);
                return RedirectToAction("Index");
            }
            var categories = _categoryService.GetAllCategories();
            var suppliers = _supplierService.GetAllSuppliers();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
            return View();

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if(product == null)
            {
                return NotFound();
            }
            var categories = _categoryService.GetAllCategories();
            var suppliers = _supplierService.GetAllSuppliers();
            ViewBag.Categories = new SelectList(categories,"Id","Name",product.CategoryId);
            ViewBag.Suppliers = new SelectList(suppliers, "Id","Name",product.SupplierId);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product);
                return RedirectToAction("Index");
            }

            var categories = _categoryService.GetAllCategories();
            var suppliers = _supplierService.GetAllSuppliers();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name", product.SupplierId);

            return View(product);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.RemoveProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
