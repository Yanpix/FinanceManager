using FinanceManager.Model.DataAccess.Repository;
using FinanceManager.Dependencies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using FinanceManager.Model.Entities;
using FinanceManager.Infrastructure;

namespace FinanceManager.Model.DataAccess.UnitOfWork
{
    /// <summary>
    /// Implementation of the UnitOfWork pattern
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        // Repositories as injection properties
        public IDataRepository<MoneyBox> MoneyBoxesRepository { get; set; }

        public IDataRepository<Currency> CurrenciesRepository { get; set; }

        public IDataRepository<Category> CategoriesRepository { get; set; }

        public IDataRepository<Transaction> TransactionsRepository { get; set; }

        public IDataRepository<User> UsersRepository { get; set; }

        // Get repository of items of type T
        public IDataRepository<T> Repository<T>() where T : class, new()
        {
            string type = typeof(T).Name;

            switch (type)
            {
                case "MoneyBox":
                    return MoneyBoxesRepository as IDataRepository<T>;
                case "Currency":
                    return CurrenciesRepository as IDataRepository<T>;
                case "Category":
                    return CategoriesRepository as IDataRepository<T>;
                case "Transaction":
                    return TransactionsRepository as IDataRepository<T>;
                case "User":
                    return UsersRepository as IDataRepository<T>;
                default:
                    return null;
            }
        }
    }
}
