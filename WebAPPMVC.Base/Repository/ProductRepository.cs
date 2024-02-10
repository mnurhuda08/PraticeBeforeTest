using Microsoft.EntityFrameworkCore;
using WebAPPMVC.Base.Data;
using WebAPPMVC.Base.Models;

namespace WebAPPMVC.Base.Repository
{
    public class ProductRepository : IRepositoryBase<Product>
    {
        private readonly DataDbContext _context;

        public ProductRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Product>> GetAll(bool trackChanges)
        {
            return !trackChanges ? _context.Products.AsNoTracking() : _context.Products.Include(p => p.Category);
        }

        public async Task<Product> GetByID(int id, bool trackChanges)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(f => f.Id == id);   
        }

        public void Insert(Product entity)
        {
            _context.Products.Add(entity);
        }

        public void Update(Product entity)
        {

            _context.Products.Update(entity);
        }

        public void Delete(Product entity)
        {

            _context.Products.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}