using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos.Models
{
    public class MenuList : IDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? MenuIconClass { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public long ParentRoleId { get; set; }
        public long RolePermissionsTypeId { get; set; }
        public long RolePriority { get; set; }
    }
}
