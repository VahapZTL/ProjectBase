using System.Linq.Expressions;
using Core.Constants;
using Core.Entities;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
    {
        public IResult Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                return new SuccessResult();
            }
        }

        public IResult Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
                return new SuccessResult();
            }
        }

        public IDataResult<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                var data = context.Set<TEntity>().Where(x => x.StatusId == 1).FirstOrDefault(filter);
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
                    : context.Set<TEntity>().Where(x => x.StatusId == 1).Where(filter).ToList();
                return new SuccessDataResult<List<TEntity>>(data);
            }
        }

        public IResult Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                return new SuccessResult();
            }
        }
    }
}
