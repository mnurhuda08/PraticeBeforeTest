using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using WebAPPMVC.Base.Models;
using WebAPPMVC.Base.Models.DTOs.CategoryDTO;
using WebAPPMVC.Base.Repository;
using WebAPPMVC.Base.Services.CategoryService;

namespace WebAPPMVC.Base.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IRepositoryBase<Category> _repoBase;
        private readonly ICategoryService<CategoryDTOs> _service;

        public CategoriesController(ICategoryService<CategoryDTOs> service)
        {
            _service = service;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll(true));
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _service.GetAll(true).Result.FirstOrDefault(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,Description,Photo")] CategoryCreateDTOs category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var file = category.Photo;
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
                        var categoryDto = new CategoryDTOs
                        {
                            CategoryName = category.CategoryName,
                            Description = category.Description,
                            Photo = fileName
                        };
                        _service.Create(categoryDto);

                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _service.GetByID((int)id, true);
            var categoryCreateDTOs = new CategoryCreateDTOs
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                Description = category.Description
            };

            if (category == null)
            {
                return NotFound();
            }
            return View(categoryCreateDTOs);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName,Description,Photo")] CategoryCreateDTOs category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var file = category.Photo;
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
                        var categoryDto = new CategoryDTOs
                        {
                            Id = category.Id,
                            CategoryName = category.CategoryName,
                            Description = category.Description,
                            Photo = fileName
                        };
                        _service.Update(categoryDto);

                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var category = await _categoryService.FindById((int)id,true);

            var category = _service.GetAll(true).Result.FirstOrDefault(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return Problem("Entity set 'RepositoryDbContext.Categories'  is null.");
            }
            //var category = await _categoryService.FindById((int)id,true);
            var category = _service.GetAll(true).Result.FirstOrDefault(m => m.Id == id);
            if (category != null)
            {
                _service.Delete(category);
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return (_service.GetAll(true)?.Result.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}