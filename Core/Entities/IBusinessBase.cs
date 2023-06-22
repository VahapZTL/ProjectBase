using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public interface IBusinessBase<T> 
        where T : class, new()
    {
        IDataResult<T> GetById(long id);
        IDataResult<List<T>> GetList();
        IResult Add(T entity);
        IResult Delete(T entity);
        IResult Update(T entity);
    }
}
