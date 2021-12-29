using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace UnitOfWork.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _db;
        private IDbContextTransaction _transaction;

        public UnitOfWork(DbContext db)
        {
            _db = db;
        }

        public void Dispose()
        {
            RollbackTransaction();

            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();

            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
            {
                Console.WriteLine("Завершите текущую транзакцию");
                return;
            }

            _transaction = _db.Database.BeginTransaction();
        }

        public void RollbackTransaction()
        {
            if (_transaction == null)
            {
                return;
            }

            _transaction.Rollback();
            _transaction.Dispose();

            _transaction = null;
        }

        public T GetRepository<T>() where T : class
        {
            if (typeof(T) == typeof(IProductRepository))
            {
                return new ProductRepository(_db) as T;
            }

            if (typeof(T) == typeof(ICategoryRepository))
            {
                return new CategoryRepository(_db) as T;
            }

            if (typeof(T) == typeof(IProductCategoryRepository))
            {
                return new ProductCategoryRepository(_db) as T;
            }

            throw new Exception("Неизвестный тип репозитория: " + typeof(T));
        }
    }
}