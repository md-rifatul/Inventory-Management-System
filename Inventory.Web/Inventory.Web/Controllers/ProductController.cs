using AutoMapper;
using Inventory.Application.Interfaces.IServices;
using Inventory.Application.ViewModels;
using Inventory.Application.ViewModels.Products;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            var vm = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View(vm);
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var suppliers = await _supplierService.GetAllSuppliersAsync();
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
        public async Task<IActionResult> Create(ProductCreateViewModel newProduct)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(newProduct);
                await _productService.AddProductAsync(product);
                return RedirectToAction("Index");
            }
            var categories = await _categoryService.GetAllCategoriesAsync();
            var suppliers = await _supplierService.GetAllSuppliersAsync();
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
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            var categories = await _categoryService.GetAllCategoriesAsync();
            var suppliers = await _supplierService.GetAllSuppliersAsync();

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
        public async Task<IActionResult> Edit(ProductEditViewModel productEditView)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(productEditView);
                await _productService.UpdateProductAsync(product);
                return RedirectToAction("Index");
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            var suppliers = await _supplierService.GetAllSuppliersAsync();
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
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();
            var vm = _mapper.Map<ProductDeleteViewModel>(product);

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            await _productService.RemoveProductAsync(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> LowStock()
        {
            var products = await _productService.GetMinimumStockLevelsAsync();
            var vm = _mapper.Map<IEnumerable<ProductViewLowStock>>(products);
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> AddStock(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            var vm = _mapper.Map<AddStockViewModel>(product);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddStock(AddStockViewModel addStockViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addStockViewModel);
            }
            await _productService.AddStockAsync(addStockViewModel.ProductId, addStockViewModel.AddQuantity);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Search(string productName)
        {
            var products = await _productService.SearchProductsAsync(productName);
            var vm = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View("Index", vm);
        }
    }
}
