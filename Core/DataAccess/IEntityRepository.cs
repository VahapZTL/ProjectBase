using System.Linq.Expressions;
using Core.Entities;
using Core.Utilities.Results;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        IResult AddRange(IList<T> entity);
        bool Any(Expression<Func<T, bool>> filter);
        IResult UpdateRange(IList<T> entity);
        IDataResult<T> Get(Expression<Func<T, bool>> filter);
        IDataResult<List<T>> GetList(Expression<Func<T, bool>> filter = null);
        IDataResult<T> Add(T entity);
        IDataResult<T> Update(T entity);
        IDataResult<T> Delete(T entity);
    }
}
