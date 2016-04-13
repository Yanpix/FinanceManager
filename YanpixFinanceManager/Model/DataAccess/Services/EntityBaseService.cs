using System.Collections.Generic;
using YanpixFinanceManager.Model.DataAccess.UnitOfWork;
using YanpixFinanceManager.Model.Entities.Common;

namespace YanpixFinanceManager.Model.DataAccess.Services
{
    public class EntityBaseService<TEntity> : IEntityBaseService<TEntity> where TEntity : class, IEntityBase, new()
    {
        #region Fields & Constructor

        private IUnitOfWork _unitOfWork;

        public EntityBaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Delete Methods

        public int Delete(int id)
        {
            return _unitOfWork.Repository<TEntity>().Delete(id);
        }

        public int DeleteAll()
        {
            return _unitOfWork.Repository<TEntity>().DeleteAll();
        }

        #endregion

        #region Get Methods

        public TEntity Get(int id, bool withChildren = true)
        {
            return _unitOfWork.Repository<TEntity>().Get(id, withChildren);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _unitOfWork.Repository<TEntity>().GetAll();
        }

        #endregion

        #region Insert Methods

        public int Insert(TEntity entity, bool replace = true, bool withChildren = false)
        {
            return _unitOfWork.Repository<TEntity>().Insert(entity, replace, withChildren);
        }

        public void InsertAll(IEnumerable<TEntity> entities, bool replace = true, bool withChildren = false)
        {
            _unitOfWork.Repository<TEntity>().InsertAll(entities, replace, withChildren);
        }

        #endregion

        #region Update Methods

        public void Update(TEntity entity, bool withChildren = true)
        {
            _unitOfWork.Repository<TEntity>().Update(entity, withChildren);
        }

        public void UpdateAll(IEnumerable<TEntity> entities, bool withChildren = true)
        {
            _unitOfWork.Repository<TEntity>().UpdateAll(entities, withChildren);
        }

        #endregion
    }
}
