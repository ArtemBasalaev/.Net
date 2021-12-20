namespace UnitOfWork.Model
{
    public interface IRepository<T> where T : class
    {
        void Create (T entity);
        
        void Delete(T entity);

        void Update(T entity);

        T[] GetAll();

        T GetById (int id);
    }
}