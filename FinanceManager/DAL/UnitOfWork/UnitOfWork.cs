using System;
using System.Collections.Generic;
using System.Linq;
using FinanceManager.DAL.Models;
using FinanceManager.DAL.Repository;
using FinanceManager.DAL.SQLite;

namespace FinanceManager.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SQLiteAsyncConnection _connection;
        private readonly SQLiteConnection _connection2;
        private Dictionary<string, object> _repositories;
        private const string ExpenseCategory = "ExpenseCategory";
        private const string IncomeCategory = "IncomeCategory";

        public void InitializateDb()
        {
            using (_connection2)
            {
                var info = _connection2.GetTableInfo(ExpenseCategory);
                if (!info.Any())
                {
                    _connection2.CreateTable<ExpenseCategory>();
                }
                info = _connection2.GetTableInfo(IncomeCategory);
                if (!info.Any())
                {
                    _connection2.CreateTable<IncomeCategory>();
                }

            }
            
        }
        public UnitOfWork()
        {
            _connection = new SQLiteAsyncConnection(App.ConnectionString);
            _connection2 = new SQLiteConnection(App.ConnectionString);
            InitializateDb();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, new()
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _connection);
                _repositories.Add(type, repositoryInstance);
            }

            return (GenericRepository<TEntity>)_repositories[type];
        }
    }
}