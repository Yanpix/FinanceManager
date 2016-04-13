using System;
using YanpixFinanceManager.Model.DataAccess.Repositories;
using YanpixFinanceManager.Model.Entities.Common;

namespace YanpixFinanceManager.Model.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IEntityBaseRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntityBase, new();
    }
}
