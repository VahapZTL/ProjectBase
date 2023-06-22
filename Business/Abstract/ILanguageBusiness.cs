using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface ILanguageBusiness : IBusinessBase<Language>
    {
        IDataResult<Language> Get(Expression<Func<Language, bool>> filter);
    }
}
