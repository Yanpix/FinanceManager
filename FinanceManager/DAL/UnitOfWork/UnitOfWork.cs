using System;
using System.Collections.Generic;
using FinanceManager.DAL.Repository;
using FinanceManager.DAL.SQLite;

namespace FinanceManager.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SQLiteAsyncConnection _connection;
        private Dictionary<string, object> _repositories;

        public UnitOfWork()
        {
            _connection = new SQLiteAsyncConnection(App.ConnectionString);
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