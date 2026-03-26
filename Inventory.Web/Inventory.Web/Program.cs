using Inventory.Application.Interfaces.IRepository;
using Inventory.Application.Interfaces.IServices;
using Inventory.Application.Mappings;
using Inventory.Application.Services;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAutoMapper(typeof(MappingProfile));

//Repo
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IStockTransactionRepository, StockTransactionRepository>();
builder.Services.AddScoped<IPurchaseOrderRepository,PurchaseOrderRepository>();
builder.Services.AddScoped<ISalesOrderRepository,SalesOrderRepository>();
builder.Services.AddScoped<ISalesOrderItemRepository,SalesOrderItemRepository>();
builder.Services.AddScoped<IOrderConfirmationRepository, OrderConfirmationRepository>();

//Services
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IStockTransactionService, StockTransactionService>();
builder.Services.AddScoped<IPurchaseOrderService,PurchaseOrderService>();
builder.Services.AddScoped<ISalesOrderService,SalesOrderService>();
builder.Services.AddScoped<IOrderConfirmationService, OrderConfirmationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
