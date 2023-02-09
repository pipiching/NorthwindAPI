using System;
using System.Data;

namespace Northwind.Repository.UnitOfWork
{
    public interface IUnitOfWorkBase : IDisposable
    {
        IDbTransaction Transaction { get; }

        void BeginTransaction();

        void Commit();

        void RollBack();
    }
}
