using Business.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class LanguageBusiness : ILanguageBusiness
    {
        private ILanguageRepository languageRepository;

        public LanguageBusiness(ILanguageRepository languageRepository)
        {
            this.languageRepository = languageRepository;
        }

        public IDataResult<Language> Get(Expression<Func<Language, bool>> filter)
        {
            return languageRepository.Get(filter);
        }
    }
}
