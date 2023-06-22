using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class LanguageBusiness : ILanguageBusiness
    {
        private ILanguageDal languageDal;

        public LanguageBusiness(ILanguageDal languageDal)
        {
            this.languageDal = languageDal;
        }

        public IResult Add(Language language)
        {
            return languageDal.Add(language);
        }

        public IResult Delete(Language language)
        {
            return languageDal.Delete(language);
        }

        public IDataResult<Language> Get(Expression<Func<Language, bool>> filter)
        {
            return languageDal.Get(filter);
        }

        public IDataResult<Language> GetById(long id)
        {
            return languageDal.Get(x => x.Id == id);
        }

        public IDataResult<List<Language>> GetList(Expression<Func<Language, bool>> filter)
        {
            return languageDal.GetList(filter);
        }

        public IDataResult<List<Language>> GetList()
        {
            return languageDal.GetList();
        }

        public IResult Update(Language language)
        {
            return languageDal.Update(language);
        }
    }
}
