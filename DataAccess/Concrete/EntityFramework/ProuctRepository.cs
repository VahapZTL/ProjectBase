using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class ProductRepository : EntityRepositoryBase<Product, ProjectBaseContext>, IProductRepository
    {
       
    }
}
