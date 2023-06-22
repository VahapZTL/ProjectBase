using System.Linq.Expressions;
using Core.Entities;
using Core.Utilities.Results;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        IDataResult<T> Get(Expression<Func<T, bool>> filter);
        IDataResult<List<T>> GetList(Expression<Func<T, bool>> filter=null);
        IResult Add(T entity);
        IResult Update(T entity);
        IResult Delete(T entity);
    }
}
