using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Model.DataAccess.Services;
using FinanceManager.Model.Entities;

namespace FinanceManager.Model.DataAccess.Providers
{
    public class DataServicesProvider : IDataServicesProvider
    {
        #region Services

        public IDataService<MoneyBox> MoneyBoxesDataService { get; set; }

        public IDataService<Currency> CurrenciesDataService { get; set; }

        public IDataService<Category> CategoriesDataService { get; set; }

        public IDataService<Transaction> TransactionsDataService { get; set; }

        public IDataService<User> UsersDataService { get; set; }

        #endregion

        public IDataService<T> Get<T>() where T : class, new()
        {
            string type = typeof(T).Name;

            switch (type)
            {
                case "MoneyBox":
                    return MoneyBoxesDataService as IDataService<T>;
                case "Currency":
                    return CurrenciesDataService as IDataService<T>;
                case "Category":
                    return CategoriesDataService as IDataService<T>;
                case "Transaction":
                    return TransactionsDataService as IDataService<T>;
                case "User":
                    return UsersDataService as IDataService<T>;
                default:
                    return null;
            }
        }
    }
}
