using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("Language", Schema = "parameters")]
    public class Language : IEntity
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public int StatusId { get; set; }

        public DateTime CreatedDate { get; set; }

        public long CreatedUserId { get; set; }

        public DateTime ModifiedDate { get; set; }

        public long ModifiedUserId { get; set; }

        public long? RolePriority { get; set; }
    }
}
