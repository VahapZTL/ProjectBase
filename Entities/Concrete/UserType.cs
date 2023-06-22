using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("UserType", Schema = "parameters")]
    public class UserType : IEntity
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int StatusId { get; set; }

        public DateTime CreatedDate { get; set; }

        public long CreatedUserId { get; set; }

        public DateTime ModifiedDate { get; set; }

        public long ModifiedUserId { get; set; }
    }
}
