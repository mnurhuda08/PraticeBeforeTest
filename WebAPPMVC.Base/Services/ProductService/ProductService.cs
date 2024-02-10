using AutoMapper;
using WebAPPMVC.Base.Models;
using WebAPPMVC.Base.Models.DTOs.ProductDTO;
using WebAPPMVC.Base.Repository;

namespace WebAPPMVC.Base.Services.ProductService
{
    public class ProductService : IProductService<ProductDTOs>
    {
        private readonly IRepositoryBase<Product> _repo;
        private readonly IMapper _mapper;

        public ProductService(IRepositoryBase<Product> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTOs>> GetAll(bool trackChanges)
        {
            var products = await _repo.GetAll(false);
            var productsDTO = products.Select(p => new ProductDTOs
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Price = p.Price,
                Stock = p.Stock,
                Photo = p.Photo,
                CategoryID = p.CategoryID,
            });
            return productsDTO;
        }

        public async Task<ProductDTOs> GetByID(int id, bool trackChanges)
        {
            var product = await _repo.GetByID(id, false);
            if (product == null)
            {
                return null;
            }
            var productDTO = new ProductDTOs
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Stock = product.Stock,
                Photo = product.Photo,
                CategoryID = product.CategoryID,
            };
            return productDTO;
        }

        public void Create(ProductDTOs entityDTOs)
        {
            var newProduct = new Product
            {
                ProductName = entityDTOs.ProductName,
                Price = entityDTOs.Price,
                Stock = entityDTOs.Stock,
                Photo = entityDTOs.Photo,
                CategoryID = entityDTOs.CategoryID,
            };
            _repo.Insert(newProduct);
            _repo.Save();
        }

        public void Update(ProductDTOs entityDTOs)
        {
            var updateProduct = new Product
            {
                Id = entityDTOs.Id,
                ProductName = entityDTOs.ProductName,
                Price = entityDTOs.Price,
                Stock = entityDTOs.Stock,
                Photo = entityDTOs.Photo,
                CategoryID = entityDTOs.CategoryID,
            };
            _repo.Update(updateProduct);
            _repo.Save();
        }

        public void Delete(ProductDTOs entityDTOs)
        {
            var product = _mapper.Map<Product>(entityDTOs);
            _repo.Delete(product);
            _repo.Save();
        }
    }
}