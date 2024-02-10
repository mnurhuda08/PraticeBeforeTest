using Microsoft.EntityFrameworkCore;
using WebAPPMVC.Base.Data;
using WebAPPMVC.Base.Models;

namespace WebAPPMVC.Base.Repository
{
    public class CategoryRepository : IRepositoryBase<Category>
    {
        private readonly DataDbContext _context;

        public CategoryRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Category>> GetAll(bool trackChanges)
        {
            return !trackChanges ? _context.Categories.AsNoTracking() : _context.Categories;
        }

        public async Task<Category> GetByID(int id, bool trackChanges)
        {
            return await _context.Categories.FindAsync(id);   
        }

        public void Insert(Category entity)
        {
            _context.Categories.Add(entity);
        }

        public void Update(Category entity)
        {

            _context.Categories.Update(entity);
        }

        public void Delete(Category entity)
        {

            _context.Categories.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}