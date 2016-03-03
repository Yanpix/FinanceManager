using FinanceManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using SQLiteNetExtensions.Extensions;
using Windows.Storage;

namespace FinanceManager.DAL
{
    public static class FinanceDatabaseHelper
    {
        /// <summary>
        /// Creates database
        /// </summary>
        /// <param name="databasePath">Database name</param>
        public static void CreateDatabase()
        {
            if (!DatabaseExist().Result)
            {
                using (var database = new SQLiteConnection(new SQLitePlatformWinRT(), App.databasePath))
                {
                    database.CreateTable<MoneyBox>();
                    database.CreateTable<Currency>();
                    database.CreateTable<Transaction>();
                    database.CreateTable<Category>();
                    database.CreateTable<User>();

                    var usDollar = new Currency
                    {
                        Name = "US Dollar",
                        Symbol = "$",
                    };

                    var gbPound = new Currency
                    {
                        Name = "GB Pound",
                        Symbol = "£",
                    };

                    database.Insert(usDollar);
                    database.Insert(gbPound);
                }               
            }
        }

        public static List<Currency> GetCurrencies()
        {
            if (DatabaseExist().Result)
            {
                using (var database = new SQLiteConnection(new SQLitePlatformWinRT(), App.databasePath))
                {
                    return database.GetAllWithChildren<Currency>();
                }
            }
            else return null;
        }

        public static MoneyBox CreateMoneyBox(string name, string currencyName)
        {
            if (DatabaseExist().Result)
            {
                using (var database = new SQLiteConnection(new SQLitePlatformWinRT(), App.databasePath))
                {
                    Currency currency = GetCurrencies().Where(i => i.Name == currencyName).SingleOrDefault();

                    MoneyBox moneyBox = new MoneyBox()
                    {
                        Balance = 0,
                        CreationDate = DateTime.Now,
                        Name = name,
                    };

                    database.Insert(moneyBox);

                    currency.MoneyBoxes = new List<MoneyBox> { moneyBox };

                    database.UpdateWithChildren(currency);

                    return moneyBox;
                }
            }
            else
                return null;
        }

        public static List<MoneyBox> GetMoneyBoxes()
        {
            if (DatabaseExist().Result)
            {
                using (var database = new SQLiteConnection(new SQLitePlatformWinRT(), App.databasePath))
                {
                    return database.GetAllWithChildren<MoneyBox>();
                }
            }
            else
                return null;
        } 

        /// <summary>
        /// Checks if database exists
        /// </summary>
        /// <param name="databasePath">Database name (without path)</param>
        /// <returns>Task<bool>, where true = exists, false = doesn't exist</returns>
        private static async Task<bool> DatabaseExist()
        {
            try
            {
                var result = await ApplicationData.Current.LocalFolder.GetItemAsync(App.databaseName) as IStorageFile;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
