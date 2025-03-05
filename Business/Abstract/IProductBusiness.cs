using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProductBusiness
    {
        IDataResult<List<Product>> GetListByCategory(int categoryId);
    }
}
