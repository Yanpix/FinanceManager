using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using YanpixFinanceManager.Model.Entities.Common;
using System;

namespace YanpixFinanceManager.Model.DataAccess.Repositories
{
    public class EntityBaseRepository<TEntity> : IEntityBaseRepository<TEntity> where TEntity : class, IEntityBase, new()
    {
        #region Fields & Constructor

        private SQLiteConnection _database;

        public EntityBaseRepository(SQLiteConnection database)
        {
            _database = database;

            CreateTable();
        }

        #endregion

        public int CreateTable()
        {
            if (_database.GetTableInfo(typeof(TEntity).Name).Count == 0)
                return _database.CreateTable<TEntity>();
            else
                return -1;
        }

        #region Delete Methods

        public int Delete(int id)
        {
            TEntity entity = Get(id, false);

            if (entity != null)
                return _database.Delete(entity);
            else
                return -1;
        }

        public int DeleteAll()
        {
            return _database.DeleteAll<TEntity>();
        }

        #endregion

        #region Get Methods

        public TEntity Get(int id, bool withChildren = true)
        {
            if (withChildren)
                return _database.GetWithChildren<TEntity>(id);
            else
                return _database.Get<TEntity>(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _database.GetAllWithChildren<TEntity>();
        }

        #endregion

        #region Insert Methods

        public int Insert(TEntity entity, bool replace = true, bool withChildren = false)
        {
            if (replace)
            {
                if (withChildren)
                    _database.InsertOrReplaceWithChildren(entity);
                else
                    _database.InsertOrReplace(entity);
            }
            else
            {
                if (withChildren)
                    _database.InsertWithChildren(entity);
                else
                    _database.Insert(entity);
            }

            return entity.Id;
        }

        public void InsertAll(IEnumerable<TEntity> entities, bool replace = true, bool withChildren = false)
        {
            if (replace)
            {
                if (withChildren)
                    _database.InsertOrReplaceAllWithChildren(entities);
                else
                    _database.InsertOrReplaceAll(entities);
            }
            else
            {
                if (withChildren)
                    _database.InsertAllWithChildren(entities);
                else
                    _database.InsertAll(entities);
            }
        }

        #endregion

        #region Update Methods

        public void Update(TEntity entity, bool withChildren = true)
        {
            if (withChildren)
                _database.UpdateWithChildren(entity);
            else
                _database.Update(entity);
        }

        public void UpdateAll(IEnumerable<TEntity> entities, bool withChildren = true)
        {
            if (!withChildren)
                _database.UpdateAll(entities);
            else
            {
                foreach (TEntity entity in entities)
                {
                    _database.UpdateWithChildren(entity);
                }
            }
        }

        #endregion
    }
}