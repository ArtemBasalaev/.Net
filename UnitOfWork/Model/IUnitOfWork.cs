using System;

namespace UnitOfWork.Model
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        T GetRepository<T>() where T : class;
    }
}