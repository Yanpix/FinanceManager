using SQLite.Net;
using System;
using System.Collections.Generic;
using YanpixFinanceManager.Model.DataAccess.Repositories;
using YanpixFinanceManager.Model.Entities.Common;

namespace YanpixFinanceManager.Model.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields & Constructor

        private bool _disposed;

        private SQLiteConnection _database;

        private Dictionary<string, object> _repositories;

        public UnitOfWork()
        {
            _database = new SQLiteConnection(App.platform, App.databasePath);
        }

        #endregion

        public IEntityBaseRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntityBase, new()
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, object>();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(EntityBaseRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _database);
                _repositories.Add(type, repositoryInstance);
            }

            return (EntityBaseRepository<TEntity>)_repositories[type];
        }

        #region Disposing

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _database.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
