using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class DescriptionBusiness : IDescriptionBusiness
    {
        private IDescriptionDal descriptionDal;

        public DescriptionBusiness(IDescriptionDal descriptionDal)
        {
            this.descriptionDal = descriptionDal;
        }

        public IResult Add(Description entity)
        {
            return descriptionDal.Add(entity);
        }

        public IResult Delete(Description entity)
        {
            return descriptionDal.Delete(entity);
        }

        public IDataResult<Description> Get(Expression<Func<Description, bool>> filter)
        {
            return descriptionDal.Get(filter); 
        }

        public IDataResult<Description> GetById(long id)
        {
            return descriptionDal.Get(x => x.Id == id);
        }

        public IDataResult<List<Description>> GetList(Expression<Func<Description, bool>> filter)
        {
            return descriptionDal.GetList(filter); 
        }

        public IDataResult<List<Description>> GetList()
        {
            return descriptionDal.GetList();
        }

        public IResult Update(Description entity)
        {
            return descriptionDal.Update(entity);   
        }
    }
}
