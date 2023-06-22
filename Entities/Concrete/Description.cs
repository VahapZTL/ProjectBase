using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("Description", Schema = "parameters")]
    public class Description : IEntity
    {
        [Key]
        public long Id { get; set; }

        public long RolePermissionsId { get; set; }

        public long LanguageId { get; set; }

        public string Value { get; set; }

        public int StatusId { get; set; }

        public DateTime CreatedDate { get; set; }

        public long CreatedUserId { get; set; }

        public DateTime ModifiedDate { get; set; }

        public long ModifiedUserId { get; set; }
    }
}
