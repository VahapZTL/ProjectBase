﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class RolePermissionsRepository : EntityRepositoryBase<RolePermissions, ProjectBaseContext>, IRolePermissionsRepository
    {
    }
}
