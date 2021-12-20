namespace UnitOfWork.Model
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetMostPopularProduct();
    }
}