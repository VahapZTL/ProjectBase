using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class RolePermissionsBusiness : IRolePermissionsBusiness
    {
        private IRolePermissionsDal rolePermissionsDal;

        public RolePermissionsBusiness(IRolePermissionsDal rolePermissionsDal)
        {
            this.rolePermissionsDal = rolePermissionsDal;
        }

        public IResult Add(RolePermissions entity)
        {
            return rolePermissionsDal.Add(entity);
        }

        public IResult Delete(RolePermissions entity)
        {
            return rolePermissionsDal.Delete(entity);
        }

        public IDataResult<RolePermissions> GetById(long id)
        {
            return rolePermissionsDal.Get(x => x.Id == id);
        }

        public IDataResult<List<RolePermissions>> GetList()
        {
            return rolePermissionsDal.GetList();
        }

        public IDataResult<List<RolePermissions>> GetList(Expression<Func<RolePermissions, bool>> filter)
        {
            return rolePermissionsDal.GetList(filter);
        }

        public IResult Update(RolePermissions entity)
        {
            return rolePermissionsDal.Update(entity);
        }
    }
}
