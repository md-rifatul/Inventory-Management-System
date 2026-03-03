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
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }
        public IActionResult Create()
        {
            var categories = _categoryService.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
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
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
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
            ViewBag.Categories = new SelectList(categories,"Id","Name",product.CategoryId);
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
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);

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
