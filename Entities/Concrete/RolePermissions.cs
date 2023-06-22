using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("RolePermissions", Schema = "auth")]
    public class RolePermissions : IEntity
    {
        [Key]
        public long Id { get; set; }

        public long? RolePermissionsTypeId { get; set; }

        public string Name { get; set; }

        public string MenuIconClass { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public long? ParentRoleId { get; set; }

        public int StatusId { get; set; }

        public DateTime CreatedDate { get; set; }

        public long CreatedUserId { get; set; }

        public DateTime ModifiedDate { get; set; }

        public long ModifiedUserId { get; set; }

        public long? RolePriority { get; set; }
    }
}
