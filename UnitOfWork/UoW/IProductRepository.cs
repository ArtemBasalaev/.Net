using UnitOfWork.Model;

namespace UnitOfWork.UoW
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetMostPopularProduct();

        Product GetMostUnpopularProduct();
    }
}