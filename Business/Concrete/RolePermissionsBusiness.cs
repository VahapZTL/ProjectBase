using Business.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Dtos.Request;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class RolePermissionsBusiness : IRolePermissionsBusiness
    {
        private IRolePermissionsRepository rolePermissionsRepository;

        public RolePermissionsBusiness(IRolePermissionsRepository rolePermissionsRepository)
        {
            this.rolePermissionsRepository = rolePermissionsRepository;
        }

        [CacheRemoveAspect("RolePermissionsBusiness.GetAllRoles()")]
        public IResult AddRoleData(RequestSetRoles req)
        {
            RolePermissions rolePermissions = new RolePermissions 
            { 
                ActionName = req.ActionName,
                ControllerName = req.ControllerName,
                MenuIconClass = req.MenuIconClass,
                CreatedDate = DateTime.Now,
                CreatedUserId = -1,
                ModifiedDate = DateTime.Now,
                ModifiedUserId = -1,
                Name = req.Name,
                ParentRoleId = req.ParentRoleId,
                RolePermissionsTypeId = req.RolePermissionsTypeId,
                RolePriority = req.RolePriority,
                StatusId = req.StatusId
            };
            rolePermissionsRepository.Add(rolePermissions);
            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<List<RolePermissions>> GetAllRoles(long roleType)
        {
            var data = rolePermissionsRepository.GetList(x => x.RolePermissionsTypeId == roleType);

            return data;
        }

        [CacheAspect]
        public IDataResult<List<RolePermissions>> GetAllRolesData(Expression<Func<RolePermissions, bool>> filter = null)
        {
            var data =  rolePermissionsRepository.GetList(x => x.StatusId == 1);
            return new SuccessDataResult<List<RolePermissions>>(data.Data, "Success");
        }
    }
}
