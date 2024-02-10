namespace WebAPPMVC.Base.Repository
{
    public interface IRepositoryBase<T>
    {
        Task<IQueryable<T>> GetAll(bool trackChanges);
        Task<T> GetByID(int id, bool trackChanges);

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();

    }
}
