using AutoMapper;
using WebAPPMVC.Base.Models;
using WebAPPMVC.Base.Models.DTOs.CategoryDTO;
using WebAPPMVC.Base.Repository;

namespace WebAPPMVC.Base.Services.CategoryService
{
    public class CategoryService : ICategoryService<CategoryDTOs>
    {
        private readonly IRepositoryBase<Category> _repo;
        private readonly IMapper _mapper;

        public CategoryService(IRepositoryBase<Category> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTOs>> GetAll(bool trackChanges)
        {
            var categories = await _repo.GetAll(false);
            var categoriesDTO = _mapper.Map<IEnumerable<CategoryDTOs>>(categories);
            return categoriesDTO;
        }

        public async Task<CategoryDTOs> GetByID(int id, bool trackChanges)
        {
            var category = await _repo.GetByID(id, false);
            var categoryDTO = _mapper.Map<CategoryDTOs>(category);
            return categoryDTO;
        }

        public void Create(CategoryDTOs entityDTOs)
        {
            var category = _mapper.Map<Category>(entityDTOs);
            _repo.Insert(category);
            _repo.Save();
        }

        public void Update(CategoryDTOs entityDTOs)
        {
            var category = _mapper.Map<Category>(entityDTOs);
            _repo.Update(category);
            _repo.Save();
        }

        public void Delete(CategoryDTOs entityDTOs)
        {
            var category = _mapper.Map<Category>(entityDTOs);
            _repo.Delete(category);
            _repo.Save();
        }
    }
}