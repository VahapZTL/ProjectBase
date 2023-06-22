using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProductBusiness : IBusinessBase<Product>
    {
        IDataResult<List<Product>> GetListByCategory(int categoryId);
    }
}
