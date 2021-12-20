using System;

namespace UnitOfWork.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        T GetRepository<T>() where T : class;
    }
}