using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FinanceManager.DAL.SQLite;

namespace FinanceManager.DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly SQLiteAsyncConnection _connection;
        private readonly SQLiteConnection _connection2;

        public GenericRepository(SQLiteAsyncConnection connection)
        {
            _connection = connection;
            _connection2 = new SQLiteConnection(App.ConnectionString);
        }

        public async Task<ObservableCollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> orderBy = null)
        {
            var query = _connection.Table<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                await query.OrderBy(orderBy).ToListAsync();
            }
            return new ObservableCollection<TEntity>(await query.ToListAsync());
        }

        public ObservableCollection<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, object>> orderBy = null)
        {
            var query = _connection2.Table<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                query.OrderBy(orderBy).ToList();
            }
            return new ObservableCollection<TEntity>(query.ToList());

        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _connection.FindAsync<TEntity>(id);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _connection.InsertAsync(entity);
        }

        public void Create(TEntity entity)
        {
            _connection2.Insert(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _connection.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            await _connection.DeleteAsync(entity);
        }

        public async Task DeleteAll()
        {
            await _connection.DropTableAsync<TEntity>();
            await _connection.CreateTableAsync<TEntity>();
        }
    }
}
