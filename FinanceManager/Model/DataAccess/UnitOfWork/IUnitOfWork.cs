using FinanceManager.Model.DataAccess.Repository;
using System.Threading.Tasks;

namespace FinanceManager.Model.DataAccess.UnitOfWork
{
    /// <summary>
    /// Unit of work
    /// </summary>
    public interface IUnitOfWork
    {
        // Get repository of items of type T
        IDataRepository<T> Repository<T>() where T : class, new();
    }
}
