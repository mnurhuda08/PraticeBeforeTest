using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAPPMVC.Base.Data;
using WebAPPMVC.Base.Models;
using WebAPPMVC.Base.Models.DTOs.CategoryDTO;
using WebAPPMVC.Base.Models.DTOs.ProductDTO;
using WebAPPMVC.Base.Repository;
using WebAPPMVC.Base.Services.CategoryService;
using WebAPPMVC.Base.Services.ProductService;

namespace WebAPPMVC.Base.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRepositoryBase<Product> _repoBase;
        private readonly IProductService<ProductDTOs> _service;
        private readonly DataDbContext _context;

        public ProductsController(IProductService<ProductDTOs> service, DataDbContext context)
        {
            _service = service;
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll(true));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _service.GetAll(true).Result.FirstOrDefault(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Price,Stock,Photo,CategoryID")] ProductCreateDTOs product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var file = product.Photo;
                    var folderName = Path.Combine("Resources", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        //collect data from dto dan filename
                        var productDto = new ProductDTOs
                        {
                            Id = product.Id,
                            ProductName = product.ProductName,
                            Price = product.Price,
                            Stock = product.Stock,
                            Photo = fileName,
                            CategoryID = product.CategoryID
                        };
                        _service.Create(productDto);

                        ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryID);
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product= await _service.GetByID((int)id, true);
            var productCreateDTOs = new ProductCreateDTOs
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Stock = product.Stock,
                CategoryID = product.CategoryID
            };

            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryID);
            return View(productCreateDTOs);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Price,Stock,Photo,CategoryID")] ProductCreateDTOs product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var file = product.Photo;
                    var folderName = Path.Combine("Resources", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        //collect data from dto dan filename
                        var productDTO = new ProductDTOs
                        {
                            Id = product.Id,
                            ProductName = product.ProductName,
                            Price = product.Price,
                            Stock = product.Stock,
                            Photo = fileName,
                            CategoryID = product.CategoryID
                        };
                        _service.Update(productDTO);

                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var category = await _categoryService.FindById((int)id,true);

            var product = _service.GetAll(true).Result.FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return Problem("Entity set 'RepositoryDbContext.Categories'  is null.");
            }
            //var category = await _categoryService.FindById((int)id,true);
            var product = _service.GetAll(true).Result.FirstOrDefault(m => m.Id == id);
            if (product != null)
            {
                _service.Delete(product);
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}