using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FinanceManager.DAL.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : new()
    {
        Task<ObservableCollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> orderBy = null);
        ObservableCollection<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> orderBy = null); //test
        Task<TEntity> GetByIdAsync(int id);
        Task CreateAsync(TEntity entity);
        void Create(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task DeleteAll();
        void Delete(int id);
    }
}