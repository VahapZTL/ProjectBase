using Core.Entities;
using Core.Entities.Dtos.Request;
using Core.Utilities.Results;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IRolePermissionsBusiness
    {
        IResult AddRoleData(RequestSetRoles req);
        IDataResult<List<RolePermissions>> GetAllRoles(long roleType);
        IDataResult<List<RolePermissions>> GetAllRolesData(Expression<Func<RolePermissions, bool>> filter = null);
    }
}
