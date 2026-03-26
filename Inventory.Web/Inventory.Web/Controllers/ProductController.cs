using AutoMapper;
using Inventory.Application.Interfaces.IServices;
using Inventory.Application.ViewModels;
using Inventory.Application.ViewModels.Products;
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
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, ICategoryService categoryService, ISupplierService supplierService, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            var vm = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View(vm);
        }
        public IActionResult Create()
        {
            var categories = _categoryService.GetAllCategories();
            var suppliers = _supplierService.GetAllSuppliers();
            var model = new ProductCreateViewModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList(),
                Suppliers = suppliers.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(ProductCreateViewModel newProduct)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(newProduct);
               _productService.AddProduct(product);
                return RedirectToAction("Index");
            }
            var categories = _categoryService.GetAllCategories();
            var suppliers = _supplierService.GetAllSuppliers();
            var model = new ProductCreateViewModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList(),
                Suppliers = suppliers.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList()
            };
            return View(model);

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return NotFound();

            var categories = _categoryService.GetAllCategories();
            var suppliers = _supplierService.GetAllSuppliers();

            var vm = _mapper.Map<ProductEditViewModel>(product);

            vm.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = c.Id == product.CategoryId
            }).ToList();

            vm.Suppliers = suppliers.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
                Selected = s.Id == product.SupplierId
            }).ToList();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(ProductEditViewModel productEditView)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(productEditView);
               _productService.UpdateProduct(product);
                return RedirectToAction("Index");
            }

            var categories = _categoryService.GetAllCategories();
            var suppliers = _supplierService.GetAllSuppliers();
            var model = new ProductCreateViewModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList(),
                Suppliers = suppliers.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList()
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
                return NotFound();
            var vm = _mapper.Map<ProductDeleteViewModel>(product);

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
             _productService.RemoveProduct(id);
             return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult LowStock()
        {
            var products = _productService.GetMinimumStockLevels();
            var vm = _mapper.Map<IEnumerable<ProductViewLowStock>>(products);
            return View(vm);
        }
        [HttpGet]
        public IActionResult AddStock(int id)
        {
            var product = _productService.GetProductById(id);
            var vm = _mapper.Map<AddStockViewModel>(product);
            return View(vm);
        }
        [HttpPost]
        public IActionResult AddStock(AddStockViewModel addStockViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addStockViewModel);
            }
            _productService.AddStock(addStockViewModel.ProductId, addStockViewModel.AddQuantity);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Search(string productName)
        {
            var products = _productService.SearchProducts(productName);
            var vm = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View("Index", vm);
        }
    }
}
