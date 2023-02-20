using Northwind.Repository.Repositories.Interfaces;
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

        /// <summary>
        /// 取得某一個Entity的Repository。
        /// 如果沒有取過，會initialise一個
        /// 如果有就取得之前initialise的那個。
        /// </summary>
        /// <typeparam name="T">此Context裡面的Entity Type</typeparam>
        /// <returns>Entity的Repository</returns>
        IRepositoryBase<T> GetRepository<T>() where T : class, new();
    }
}
