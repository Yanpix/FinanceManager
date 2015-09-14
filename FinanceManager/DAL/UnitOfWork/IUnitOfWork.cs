using FinanceManager.DAL.Repository;

namespace FinanceManager.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
       IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, new()
    }
}