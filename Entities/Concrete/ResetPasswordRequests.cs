using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("ResetPasswordRequests", Schema = "auth")]
    public class ResetPasswordRequests : IEntity
    {
        [Key]
        public long Id { get; set; }

        public Guid ResetId { get; set; }

        public long? UserId { get; set; }

        public int StatusId { get; set; }

        public DateTime CreatedDate { get; set; }

        public long CreatedUserId { get; set; }

        public DateTime ModifiedDate { get; set; }

        public long ModifiedUserId { get; set; }
    }
}
