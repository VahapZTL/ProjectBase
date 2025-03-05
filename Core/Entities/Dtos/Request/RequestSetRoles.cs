using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos.Request
{
    public class RequestSetRoles : IDto
    {

        public long? RolePermissionsTypeId { get; set; }

        public string Name { get; set; }

        public string MenuIconClass { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public long? ParentRoleId { get; set; }

        public int StatusId { get; set; }

        public long? RolePriority { get; set; }
    }
}
