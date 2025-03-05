using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class DescriptionBusiness : IDescriptionBusiness
    {
        private IDescriptionRepository descriptionRepository;

        public DescriptionBusiness(IDescriptionRepository descriptionRepository)
        {
            this.descriptionRepository = descriptionRepository;
        }

        public IDataResult<Description> Get(Expression<Func<Description, bool>> filter)
        {
            return descriptionRepository.Get(filter); 
        }
    }
}
