using System.Linq;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Model;

namespace UnitOfWork.UoW
{
    public class ProductRepository : BaseEfRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext db) : base(db)
        {
        }

        public Product GetMostPopularProduct()
        {
            return DbSet
                .OrderByDescending(p => p.OrderDetailsList.Sum(od => od.Quantity))
                .FirstOrDefault();
        }
        public Product GetMostUnpopularProduct()
        {
            return DbSet
                .OrderBy(p => p.OrderDetailsList.Sum(od => od.Quantity))
                .FirstOrDefault();
        }
    }
}