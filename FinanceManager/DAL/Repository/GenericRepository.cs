using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using FinanceManager.DAL.SQLite;

namespace FinanceManager.DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly SQLiteAsyncConnection _connection;

        public GenericRepository(SQLiteAsyncConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> orderBy = null)
        {
            var query = _connection.Table<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                return await query.OrderBy(orderBy).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _connection.FindAsync<TEntity>(id);
        }

        public async Task CreateAsync(TEntity entity)
        {
             await _connection.InsertAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
             await _connection.UpdateAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
             await _connection.DeleteAsync(entity);
        }
    }
}
