using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IDescriptionBusiness
    {
        IDataResult<Description> Get(Expression<Func<Description, bool>> filter);
    }
}
