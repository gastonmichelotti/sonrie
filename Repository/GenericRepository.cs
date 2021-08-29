using Microsoft.EntityFrameworkCore;
using netCoreNew.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace netCoreNew.Repository
{
    public class GenericRepository<IEntityBase> : IGenericRepository<IEntityBase> where IEntityBase : class
    {
        protected NetCoreNewContext _context;
        protected readonly DbSet<IEntityBase> _dbset;

        public GenericRepository(NetCoreNewContext context)
        {
            _context = context;
            _dbset = _context.Set<IEntityBase>();
        }

        public IEnumerable<IEntityBase> GetAll(params Expression<Func<IEntityBase, object>>[] navigation)
        {
            IQueryable<IEntityBase> query = _dbset;

            foreach (Expression<Func<IEntityBase, object>> include in navigation)
                query = query.Include(include);

            return query;
        }

        public IEntityBase GetSingle(Expression<Func<IEntityBase, bool>> predicate, params Expression<Func<IEntityBase, object>>[] navigation)
        {
            IQueryable<IEntityBase> query = _dbset;

            foreach (Expression<Func<IEntityBase, object>> include in navigation)
                query = query.Include(include);

            return query.FirstOrDefault(predicate);
        }

        public IEnumerable<IEntityBase> GetList(Expression<Func<IEntityBase, bool>> predicate, params Expression<Func<IEntityBase, object>>[] navigation)
        {
            IQueryable<IEntityBase> query = _dbset;

            foreach (Expression<Func<IEntityBase, object>> include in navigation)
                query = query.Include(include);

            return query.Where(predicate);
        }

        public IEntityBase GetById(int id)
        {
            return _context.Set<IEntityBase>().Find(id);
        }

        public virtual void Add(IEntityBase entity)
        {
            _context.Set<IEntityBase>().Add(entity);
            Save();
        }

        public virtual void AddRange(IEntityBase[] entities)
        {
            _context.Set<IEntityBase>().AddRange(entities);
            Save();
        }

        public virtual void Delete(IEntityBase entity)
        {
            _context.Set<IEntityBase>().Remove(entity);
            Save();
        }

        public virtual void DeleteRange(IEntityBase[] entities)
        {
            _context.Set<IEntityBase>().RemoveRange(entities);
            Save();
        }

        public virtual void Edit(IEntityBase entity)
        {
            _context.Set<IEntityBase>().Update(entity);
            Save();
        }

        public virtual void EditRange(IEntityBase[] entities)
        {
            _context.Set<IEntityBase>().UpdateRange(entities);
            Save();
        }

        public virtual void Save() => _context.SaveChanges();
    }
}
