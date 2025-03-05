using System.Linq.Expressions;
using Core.Constants;
using Core.Entities;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
    {
        public IDataResult<TEntity> Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                return new SuccessDataResult<TEntity>(addedEntity.Entity);
            }
        }

        public IDataResult<TEntity> Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
                return new SuccessDataResult<TEntity>(deletedEntity.Entity);
            }
        }

        public IDataResult<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                var data = context.Set<TEntity>().Where(filter).FirstOrDefault();
                if (data is null)
                {
                    return new ErrorDataResult<TEntity>();
                }
                return new SuccessDataResult<TEntity>(data);
            }
        }

        public IDataResult<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                var data = filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
                return new SuccessDataResult<List<TEntity>>(data);
            }
        }

        public IDataResult<TEntity> Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                return new SuccessDataResult<TEntity>(updatedEntity.Entity);
            }
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                var data = context.Set<TEntity>().Any(filter);
                return data;
            }
        }

        public IResult AddRange(IList<TEntity> entity)
        {
            using (var context = new TContext())
            {
                context.AddRange(entity);
                context.SaveChanges();
                return new SuccessResult();
            }
        }

        public IResult UpdateRange(IList<TEntity> entity)
        {
            using (var context = new TContext())
            {
                context.UpdateRange(entity);
                context.SaveChanges();
                return new SuccessResult();
            }
        }
    }
}
