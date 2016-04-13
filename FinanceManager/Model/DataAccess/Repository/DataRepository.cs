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

            _platform = App.platform;

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
                    new Currency { Title = "us dollar", Symbol = "$" },
                    new Currency { Title = "gb pound",  Symbol = "₤" },
                    new Currency { Title = "euro",      Symbol = "€" },
                    new Currency { Title = "fr frank",  Symbol = "₣" }
                };

                List<Category> initialCategories = new List<Category>
                {
                    new Category { Title = "food",          Type = TransactionType.Expence },
                    new Category { Title = "salary",        Type = TransactionType.Income  },
                    new Category { Title = "gift",          Type = TransactionType.Income  },
                    new Category { Title = "gift",          Type = TransactionType.Expence },
                    new Category { Title = "rent",          Type = TransactionType.Income  },
                    new Category { Title = "rent",          Type = TransactionType.Expence },
                    new Category { Title = "entertainment", Type = TransactionType.Expence },
                    new Category { Title = "dividends",     Type = TransactionType.Income  },
                    new Category { Title = "compensation",  Type = TransactionType.Income  },
                };

                MoneyBox initialMoneyBox = new MoneyBox
                {
                    Balance = 0.0M,
                    CreationDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    Title = "test money box"
                };

                User initialUser = new User
                {
                    Title = "me", Password = "me"
                };

                MoneyBoxToUser initialMoneyBoxToUser = new MoneyBoxToUser
                {
                    MoneyBoxId = 1, UserId = 1, IsOwner = true, CanModify = true, CanRealize = true
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

                if (db.GetTableInfo("User").Count == 0)
                {
                    db.CreateTable<User>();

                    db.Insert(initialUser);
                }

                if (db.GetTableInfo("MoneyBox").Count == 0)
                {
                    db.CreateTable<MoneyBox>();

                    db.Insert(initialMoneyBox);
                }

                if (db.GetTableInfo("MoneyBoxToUser").Count == 0)
                {
                    db.CreateTable<MoneyBoxToUser>();

                    db.Insert(initialMoneyBoxToUser);

                    initialUser.MoneyBoxes = new List<MoneyBox>();

                    initialUser.MoneyBoxes.Add(db.Get<MoneyBox>(1));

                    initialMoneyBox.PrimaryCurrency = db.Get<Currency>(4);

                    initialMoneyBox.Users = new List<User>();

                    initialMoneyBox.Users.Add(db.Get<User>(1));

                    db.UpdateWithChildren(initialMoneyBox);
                }

                if (db.GetTableInfo("Transaction").Count == 0)
                {
                    db.CreateTable<Transaction>();
                }              
            }
        }

        // Create an item of type T
        public void Save(T item)
        {
            using (SQLiteConnection db = new SQLiteConnection(_platform, _path))
            {
                db.InsertOrReplaceWithChildren(item);
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
                    db.Delete(i);
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
                db.UpdateWithChildren(item);
            }
        }
    }
}
