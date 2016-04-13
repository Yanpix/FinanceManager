using System.Collections.Generic;
using YanpixFinanceManager.Model.Entities.Common;

namespace YanpixFinanceManager.Model.DataAccess.Repositories
{
    public interface IEntityBaseRepository<TEntity> where TEntity : class, IEntityBase, new()
    {
        int CreateTable();

        TEntity Get(int id, bool withChildren = true);

        IEnumerable<TEntity> GetAll();

        int Insert(TEntity entity, bool replace = true, bool withChildren = false);

        void InsertAll(IEnumerable<TEntity> entities, bool replace = true, bool withChildren = false);

        void Update(TEntity entity, bool withChildren = true);

        void UpdateAll(IEnumerable<TEntity> entities, bool withChildren = true);

        int Delete(int id);

        int DeleteAll();
    }
}
