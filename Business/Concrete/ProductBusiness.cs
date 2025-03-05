using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Business.BusinessAspects.Autofac;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class ProductBusiness : IProductBusiness
    {
        private IProductRepository productRepository;

        public ProductBusiness(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [Authorization]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return productRepository.GetList(p => p.CategoryId == categoryId);
        }
    }
}
