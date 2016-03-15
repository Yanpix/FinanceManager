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
        public IDataRepository<MoneyBox> MoneyBoxesRepository { get; set; }

        public IDataRepository<Currency> CurrenciesRepository { get; set; }





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
                default:
                    return null;
            }
        }
    }
}
