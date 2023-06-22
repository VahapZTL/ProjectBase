using Core.Entities;
using Core.Entities.Dtos.Models;
using Core.Utilities.Results;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IRolePermissionsMatchBusiness
    {
        IDataResult<RolePermissionsMatch> GetById(long id);
        IDataResult<List<RolePermissionsMatch>> GetList();
        IDataResult<List<RolePermissionsMatch>> GetList(Expression<Func<RolePermissionsMatch, bool>> filter);
        IResult Add(RolePermissionsMatch entity);
        IResult Delete(RolePermissionsMatch entity);
        IResult Update(RolePermissionsMatch entity);
        IDataResult<List<AuthorizationModel>> GetMenuList(long UserTypeId, long langId);
    }
}
