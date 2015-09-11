using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using FinanceManager.DAL.SQLite;

namespace FinanceManager.DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly SQLiteAsyncConnection _db;

        public GenericRepository()
        {
            _db = new SQLiteAsyncConnection(App.DbPath);
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> orderBy = null)
        {
            var query = _db.Table<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _db.FindAsync<TEntity>(id);
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            return await _db.InsertAsync(entity);
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            return await _db.UpdateAsync(entity);
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            return await _db.DeleteAsync(entity);
        }
    }
}
