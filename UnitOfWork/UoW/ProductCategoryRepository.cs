using Microsoft.EntityFrameworkCore;
using UnitOfWork.Model;

namespace UnitOfWork.UoW
{
    public class ProductCategoryRepository : BaseEfRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(DbContext db) : base(db)
        {
        }
    }
}