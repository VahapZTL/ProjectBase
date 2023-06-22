using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Enums;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private ICategoryDal categoryDal;

        public CategoryBusiness(ICategoryDal categoryDal)
        {
            this.categoryDal = categoryDal;
        }

        [Authorization(EnumUserType.InternLawyer, EnumUserType.Lawyer)]
        public IResult Add(Category entity)
        {
            return categoryDal.Add(entity); 
        }

        public IResult Delete(Category entity)
        {
            return categoryDal.Delete(entity);
        }

        public IDataResult<Category> GetById(long id)
        {
            return categoryDal.Get(x => x.Id == id);
        }

        public IDataResult<List<Category>> GetList()
        {
            var data = categoryDal.GetList();

            if (data.Success)
                return new SuccessDataResult<List<Category>>(categoryDal.GetList().Data, Messages.CategoryListReturned);
            else
                return new ErrorDataResult<List<Category>>(data.Message);
        }

        public IDataResult<List<Category>> GetList(Expression<Func<Category, bool>> filter)
        {
            return categoryDal.GetList(filter);
        }

        public IResult Update(Category entity)
        {
            return categoryDal.Update(entity);
        }
    }
}
