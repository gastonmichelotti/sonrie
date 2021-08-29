using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace netCoreNew.Repository
{
    public interface IGenericRepository<IEntityBase> where IEntityBase : class
    {
        IEnumerable<IEntityBase> GetAll(params Expression<Func<IEntityBase, object>>[] navigation);

        IEnumerable<IEntityBase> GetList(Expression<Func<IEntityBase, bool>> predicate, params Expression<Func<IEntityBase, object>>[] navigation);

        IEntityBase GetSingle(Expression<Func<IEntityBase, bool>> predicate, params Expression<Func<IEntityBase, object>>[] navigation);

        IEntityBase GetById(int id);

        void Add(IEntityBase entity);
        void AddRange(IEntityBase[] entities);

        void Delete(IEntityBase entity);
        void DeleteRange(IEntityBase[] entities);

        void Edit(IEntityBase entity);
        void EditRange(IEntityBase[] entities);

        void Save();
    }
}