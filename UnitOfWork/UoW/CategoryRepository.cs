using Microsoft.EntityFrameworkCore;
using UnitOfWork.Model;

namespace UnitOfWork.UoW
{
    public class CategoryRepository : BaseEfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext db) : base(db)
        {
        }
    }
}