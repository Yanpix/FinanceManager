using FinanceManager.Dependencies;
using FinanceManager.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace FinanceManager.Model.DataAccess.Repository
{
    /// <summary>
    /// Data repository
    /// </summary>
    public class DataRepository<T> : IDataRepository<T> where T : class, new()
    {
        // Connection to SQLite database
        private SQLiteConnection _connection;

        // Asynchronous connection to SQLite database
        private SQLiteAsyncConnection _connectionAsync;

        // Repository constructor: initializing connections
        public DataRepository()
        {
            _connection = new SQLiteConnection(App.databasePath);

            _connectionAsync = new SQLiteAsyncConnection(App.databasePath);

            _connection.CreateTable<MoneyBox>();

            _connection.CreateTable<Currency>();
        }

        private async Task<bool> DoesDbExist(string DatabaseName)
        {
            bool dbexist = true;
            try
            {
                StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(DatabaseName);

            }
            catch
            {
                dbexist = false;
            }

            return dbexist;
        }

        // Create an item of type T
        public void Create(T item)
        {
            if (_connection != null)
            {
                _connection.Insert(item);
            }
        }

        // Asynchronously create an item of type T
        public async Task CreateAsync(T item)
        {
            if (_connectionAsync != null)
            {
                await _connectionAsync.InsertAsync(item);
            }
        }

        // Delete an item of type T by id, if item exists
        public void Delete(int id)
        {
            if (_connection != null)
            {
                T i = _connection.Get<T>(id);

                if (i != null)
                {
                    _connection.Delete(id);
                }
            }
        }

        // Delete all items of type T
        public void DeleteAll()
        {
            if (_connection != null)
            {
                _connection.DeleteAll<T>();
            }
        }

        // Asynchronously delete all items of type T
        public async Task DeleteAllAsync()
        {
            if (_connectionAsync != null)
            {
                List<T> items = await _connectionAsync.Table<T>().ToListAsync();

                foreach (T item in items)
                {
                    await _connectionAsync.DeleteAsync(item);
                }
            }
        }

        // Asynchronously delete an item of type T by id, if item exists
        public async Task DeleteAsync(int id)
        {
            if (_connectionAsync != null)
            {
                T i = await _connectionAsync.GetAsync<T>(id);

                if (i != null)
                {
                    await _connectionAsync.DeleteAsync(id);
                }
            }
        }

        // Get an item of type T by id
        public T Get(int id)
        {
            if (_connection != null)
            {
                return _connection.Get<T>(id);
            }
            else
                return null;
        }

        // Get all items of type T
        public List<T> GetAll()
        {
            if (_connection != null)
            {
                return _connection.Table<T>().ToList();
            }
            else
                return null;
        }

        // Asynchronously get all items of type T
        public async Task<List<T>> GetAllAsync()
        {
            if (_connectionAsync != null)
            {
                return await _connectionAsync.Table<T>().ToListAsync();
            }
            else
                return null;
        }

        // Asynchronously get an item of type T by id
        public async Task<T> GetAsync(int id)
        {
            if (_connectionAsync != null)
            {
                return await _connectionAsync.GetAsync<T>(id);
            }
            else
                return null;
        }

        // Update an item of type T
        public void Update(T item)
        {
            if (_connection != null)
            {
                _connection.Update(item);
            }
        }

        // Asynchronously update an item of type T
        public async Task UpdateAsync(T item)
        {
            if (_connectionAsync != null)
            {
                await _connectionAsync.UpdateAsync(item);
            }
        }
    }
}
