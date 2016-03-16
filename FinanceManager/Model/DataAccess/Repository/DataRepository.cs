using FinanceManager.Model.Entities;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using SQLite.Net.Platform.WinRT;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using FinanceManager.Model.Entities.Enums;

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
            _connection = new SQLiteConnection(new SQLitePlatformWinRT(), App.databasePath);

            _connectionAsync = null; //new SQLiteAsyncConnection(App.databasePath);

            InitializeDatabase();
        }

        // Creates databse tables with initial content
        private void InitializeDatabase()
        {
            List<Currency> initialCurrencys;

            MoneyBox initialMoneyBox;

            List<Category> initialCategories;

            if (_connection.GetTableInfo("Currency").Count == 0)
            {
                _connection.CreateTable<Currency>();

                initialCurrencys = new List<Currency>
                {
                    new Currency
                    {
                        Title = "US Dollar",
                        Symbol = "$"
                    },
                    new Currency
                    {
                        Title = "GB Pound",
                        Symbol = "₤"
                    },
                    new Currency
                    {
                        Title = "Euro",
                        Symbol = "€"
                    },
                    new Currency
                    {
                        Title = "FR Frank",
                        Symbol = "₣"
                    }
                };

                foreach (Currency currency in initialCurrencys)
                {
                    _connection.Insert(currency);
                }
            }

            if (_connection.GetTableInfo("Category").Count == 0)
            {
                _connection.CreateTable<Category>();

                initialCategories = new List<Category>
                {
                    new Category { Title = "Food", Type = TransactionType.Expence },
                    new Category { Title = "Salary", Type = TransactionType.Income },
                    new Category { Title = "Gift", Type = TransactionType.Income },
                    new Category { Title = "Gift", Type = TransactionType.Expence },
                    new Category { Title = "Rent", Type = TransactionType.Income },
                    new Category { Title = "Rent", Type = TransactionType.Expence },
                    new Category { Title = "Entertainment", Type = TransactionType.Expence },
                    new Category { Title = "Dividends", Type = TransactionType.Income },
                    new Category { Title = "Compensation", Type = TransactionType.Income },
                };

                foreach (Category category in initialCategories)
                {
                    _connection.Insert(category);
                }
            }

            if (_connection.GetTableInfo("Transaction").Count == 0)
            {
                _connection.CreateTable<Transaction>();
            }

            if (_connection.GetTableInfo("MoneyBox").Count == 0)
            {
                _connection.CreateTable<MoneyBox>();

                initialMoneyBox = new MoneyBox
                {
                    Balance = 0.0M,
                    CreationDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    Title = "test money box"
                };

                _connection.Insert(initialMoneyBox);

                initialMoneyBox.PrimaryCurrency = _connection.Get<Currency>(2);

                _connection.UpdateWithChildren(initialMoneyBox);
            }
        }

        // Create an item of type T
        public void Create(T item)
        {
            if (_connection != null)
            {
                _connection.Insert(item);
                _connection.UpdateWithChildren(item);
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
                return _connection.GetAllWithChildren<T>().ToList();
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
