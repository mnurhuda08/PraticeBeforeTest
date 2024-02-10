using Microsoft.EntityFrameworkCore;
using WebAPPMVC.Base.Models;

namespace WebAPPMVC.Base.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}