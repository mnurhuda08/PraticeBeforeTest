using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebAPPMVC.Base.Data;
using WebAPPMVC.Base.Models;
using WebAPPMVC.Base.Models.DTOs.CategoryDTO;
using WebAPPMVC.Base.Models.DTOs.ProductDTO;
using WebAPPMVC.Base.Repository;
using WebAPPMVC.Base.Services.CategoryService;
using WebAPPMVC.Base.Services.ProductService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//register AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IRepositoryBase<Category>, CategoryRepository>();
builder.Services.AddScoped<ICategoryService<CategoryDTOs>,CategoryService>();
builder.Services.AddScoped<IRepositoryBase<Product>, ProductRepository>();
builder.Services.AddScoped<IProductService<ProductDTOs>, ProductService>();

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
// set folder resources to static file
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
