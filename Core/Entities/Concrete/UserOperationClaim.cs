using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Concrete
{
    [Table("UserOperationClaims", Schema = "auth")]
    public class UserOperationClaim : IEntity
    {
        [Key]
        public long Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long ModifiedUserId { get; set; }
    }
}
