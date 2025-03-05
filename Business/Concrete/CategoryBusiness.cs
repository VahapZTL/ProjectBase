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
        private ICategoryRepository categoryRepository;

        public CategoryBusiness(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
    }
}
