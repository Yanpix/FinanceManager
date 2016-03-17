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
        // Database path
        private string _path;

        // SQLite platform
        private SQLitePlatformWinRT _platform;

        // Repository constructor: initializing connections
        public DataRepository()
        {
            _path = App.databasePath;

            _platform = new SQLitePlatformWinRT();

            InitializeDatabase();
        }

        // Creates databse tables with initial content
        private void InitializeDatabase()
        {
            using (SQLiteConnection db = new SQLiteConnection(_platform, _path))
            {
                // Setting initial data
                List<Currency> initialCurrencys = new List<Currency>
                {
                    new Currency { Title = "US Dollar", Symbol = "$" },
                    new Currency { Title = "GB Pound",  Symbol = "₤" },
                    new Currency { Title = "Euro",      Symbol = "€" },
                    new Currency { Title = "FR Frank",  Symbol = "₣" }
                };

                List<Category> initialCategories = new List<Category>
                {
                    new Category { Title = "Food",          Type = TransactionType.Expence },
                    new Category { Title = "Salary",        Type = TransactionType.Income  },
                    new Category { Title = "Gift",          Type = TransactionType.Income  },
                    new Category { Title = "Gift",          Type = TransactionType.Expence },
                    new Category { Title = "Rent",          Type = TransactionType.Income  },
                    new Category { Title = "Rent",          Type = TransactionType.Expence },
                    new Category { Title = "Entertainment", Type = TransactionType.Expence },
                    new Category { Title = "Dividends",     Type = TransactionType.Income  },
                    new Category { Title = "Compensation",  Type = TransactionType.Income  },
                };

                MoneyBox initialMoneyBox = new MoneyBox
                {
                    Balance = 0.0M,
                    CreationDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    Title = "test money box"
                };

                // Creating tables if not exist
                if (db.GetTableInfo("Currency").Count == 0)
                {
                    db.CreateTable<Currency>();

                    foreach (Currency currency in initialCurrencys)
                    {
                        db.Insert(currency);
                    }
                }

                if (db.GetTableInfo("Category").Count == 0)
                {
                    db.CreateTable<Category>();

                    foreach (Category category in initialCategories)
                    {
                        db.Insert(category);
                    }
                }

                if (db.GetTableInfo("MoneyBox").Count == 0)
                {
                    db.CreateTable<MoneyBox>();

                    db.Insert(initialMoneyBox);

                    initialMoneyBox.PrimaryCurrency = db.Get<Currency>(4);

                    db.UpdateWithChildren(initialMoneyBox);
                }

                if (db.GetTableInfo("Transaction").Count == 0)
                {
                    db.CreateTable<Transaction>();
                }

                if (db.GetTableInfo("User").Count == 0)
                {
                    db.CreateTable<User>();
                }               
            }
        }

        // Create an item of type T
        public void Create(T item)
        {
            using (SQLiteConnection db = new SQLiteConnection(_platform, _path))
            {
                db.Insert(item);
            }
        }

        // Delete an item of type T by id, if item exists
        public void Delete(int id)
        {
            using (SQLiteConnection db = new SQLiteConnection(_platform, _path))
            {
                T i = db.Get<T>(id);

                if (i != null)
                {
                    db.Delete(id);
                }
            }
        }

        // Delete all items of type T
        public void DeleteAll()
        {
            using (SQLiteConnection db = new SQLiteConnection(_platform, _path))
            {
                db.DeleteAll<T>();
            }
        }

        // Get an item of type T by id
        public T Get(int id)
        {
            using (SQLiteConnection db = new SQLiteConnection(_platform, _path))
            {
                return db.GetWithChildren<T>(id);
            }
        }

        // Get all items of type T
        public List<T> GetAll()
        {
            using (SQLiteConnection db = new SQLiteConnection(_platform, _path))
            {
                return db.GetAllWithChildren<T>().ToList();
            }
        }

        // Update an item of type T
        public void Update(T item)
        {
            using (SQLiteConnection db = new SQLiteConnection(_platform, _path))
            {
                db.InsertOrReplace(item);
            }
        }
    }
}
