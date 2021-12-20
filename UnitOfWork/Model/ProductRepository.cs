using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UnitOfWork.Model
{
    public class ProductRepository : BaseEfRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext db) : base(db)
        {
        }

        public Product GetMostPopularProduct()
        {
            return DbSet
                .Include(p => p.OrderDetailsList)
                .OrderByDescending(p => p.OrderDetailsList.Count)
                .Take(1)
                .SingleOrDefault();
        }
    }
}