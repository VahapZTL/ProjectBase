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
        private IProductDal productDal;
        private ICategoryBusiness categoryService;

        public ProductBusiness(IProductDal productDal, ICategoryBusiness categoryService)
        {
            this.productDal = productDal;
            this.categoryService = categoryService;
        }

        [Authorization]
        public IDataResult<Product> GetById(long productId)
        {
            return productDal.Get(p => p.Id == productId);
        }

        [Authorization]
        public IDataResult<List<Product>> GetList()
        {
            return productDal.GetList();
        }

        [Authorization]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return productDal.GetList(p => p.CategoryId == categoryId);
        }


        [ValidationAspect(typeof(ProductValidator), Priority = 1)]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),CheckIfCategoryIsEnabled(product.CategoryId));

            if (result != null)
            {
                return result;
            }
            var data = productDal.Add(product);

            if (data.Success)
                return new SuccessResult(Messages.ProductAdded);
            else
                return new ErrorResult(data.Message);
        }

        private IResult CheckIfProductNameExists(string productName)
        {

            var result = productDal.GetList(p => p.ProductName == productName).Data.Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCategoryIsEnabled(long categoryId)
        {
            var result = categoryService.GetList();
            
            if (!result.Data.Any(x => x.Id == categoryId && x.StatusId == 1))
            {
                return new ErrorResult(Messages.CategoryAlreadyExist);
            }

            return new SuccessResult();
        }

        public IResult Delete(Product product)
        {
            var data = productDal.Delete(product);
            if (data.Success)
                return new SuccessResult(Messages.ProductDeleted);
            else
                return new ErrorResult(data.Message);
        }

        public IResult Update(Product product)
        {

            var data = productDal.Update(product);
            if (data.Success)
                return new SuccessResult(Messages.ProductUpdated);
            else
                return new ErrorResult(data.Message);
        }
    }
}
