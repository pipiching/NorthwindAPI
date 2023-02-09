using Northwind.Repository.Repositories.Interfaces;

namespace Northwind.Repository.UnitOfWork
{
    public interface IUnitOfWork : IUnitOfWorkBase
    {
        IProductRepository ProductRepository { get; }
    }
}
