namespace WebAPPMVC.Base.Services.ProductService
{
    public interface IProductService<TEntityDTOs>
    {
        Task<IEnumerable<TEntityDTOs>> GetAll(bool trackChanges);

        Task<TEntityDTOs> GetByID(int id, bool trackChanges);

        void Create(TEntityDTOs entityDTOs);

        void Update(TEntityDTOs entityDTOs);

        void Delete(TEntityDTOs entityDTOs);
    }
}