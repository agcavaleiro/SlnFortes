using Fortes.Models;
using Microsoft.Data.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Fortes.DAL
{
    public class BaseDAL<TEntity> : IDisposable
        where TEntity : BaseEntity
    {

        public DataContext DataContext { get; private set; }
        protected DbSet<TEntity> _dbSet;

        public BaseDAL(DataContext dataContext)
        {
            DataContext = dataContext;
            _dbSet = DataContext.Set<TEntity>();
        }
        public IQueryable<TEntity> Get
        {
            get
            {
                return _dbSet;
            }
        }
        public TEntity Find(string id)
        {
            return _dbSet.FirstOrDefault(obj => obj.Id.Equals(id));
        }
        
        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        
        public virtual void Update(TEntity entity)
        {
            DataContext.Entry(entity).State = EntityState.Modified;
        }
        
        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            _dbSet.Where(predicate).ToList().ForEach(del => _dbSet.Remove(del));
        }
        
        public int SaveChanges()
        {
            return DataContext.SaveChanges();
        }

        public void Dispose()
        {
            if (DataContext != null)
            {
                DataContext.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
    public interface IBaseDAL<TEntity>
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> Get { get; }
        TEntity Find(string id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        int SaveChanges();
    }
}
