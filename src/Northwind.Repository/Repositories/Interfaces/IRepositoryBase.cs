using Northwind.Repository.UnitOfWork;
using System.Collections.Generic;

namespace Northwind.Repository.Repositories.Interfaces
{
    public interface IRepositoryBase<TModel> where TModel : class, new()
    {
        TModel Get(dynamic id);

        IEnumerable<TModel> GetAll();

        int Insert(TModel obj, IUnitOfWorkBase uow = null);

        int Insert(IEnumerable<TModel> list, IUnitOfWorkBase uow = null);

        bool Update(TModel obj, IUnitOfWorkBase uow = null);

        bool Update(IEnumerable<TModel> list, IUnitOfWorkBase uow = null);

        bool Delete(TModel obj, IUnitOfWorkBase uow = null);

        bool Delete(IEnumerable<TModel> list, IUnitOfWorkBase uow = null);

        bool DeleteAll(IUnitOfWorkBase uow = null);

        void Dispose();
    }
}
