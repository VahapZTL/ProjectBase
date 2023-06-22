using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class RolePermissionsTypeBusiness : IRolePermissionsTypeBusiness
    {
        private IRolePermissionsTypeDal rolePermissionsTypeDal;

        public RolePermissionsTypeBusiness(IRolePermissionsTypeDal rolePermissionsTypeDal)
        {
            this.rolePermissionsTypeDal = rolePermissionsTypeDal;
        }

        public IResult Add(RolePermissionsType entity)
        {
            return rolePermissionsTypeDal.Add(entity);
        }

        public IResult Delete(RolePermissionsType entity)
        {
            return rolePermissionsTypeDal.Delete(entity);
        }

        public IDataResult<RolePermissionsType> GetById(long id)
        {
            return rolePermissionsTypeDal.Get(x => x.Id == id);
        }

        public IDataResult<List<RolePermissionsType>> GetList()
        {
            return rolePermissionsTypeDal.GetList();
        }

        public IDataResult<List<RolePermissionsType>> GetList(Expression<Func<RolePermissionsType, bool>> filter)
        {
            return rolePermissionsTypeDal.GetList(filter);
        }

        public IResult Update(RolePermissionsType entity)
        {
            return rolePermissionsTypeDal.Update(entity);
        }
    }
}
