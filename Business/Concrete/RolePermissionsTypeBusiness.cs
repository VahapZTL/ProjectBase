using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class RolePermissionsTypeBusiness : IRolePermissionsTypeBusiness
    {
        private IRolePermissionsTypeRepository rolePermissionsTypeRepository;

        public RolePermissionsTypeBusiness(IRolePermissionsTypeRepository rolePermissionsTypeRepository)
        {
            this.rolePermissionsTypeRepository = rolePermissionsTypeRepository;
        }
    }
}
