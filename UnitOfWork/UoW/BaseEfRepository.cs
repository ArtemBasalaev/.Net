using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UnitOfWork.UoW
{
    public class BaseEfRepository<T> : IRepository<T> where T : class
    {
        protected DbContext Db;
        protected DbSet<T> DbSet;

        public BaseEfRepository(DbContext db)
        {
            Db = db;

            DbSet = db.Set<T>();
        }

        public virtual void Save()
        {
            Db.SaveChanges();
        }

        public virtual void Create(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            if (Db.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            DbSet.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            DbSet.Attach(entity);

            Db.Entry(entity).State = EntityState.Modified;
        }

        public virtual T[] GetAll()
        {
            return DbSet.ToArray();
        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }
    }
}