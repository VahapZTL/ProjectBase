using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;

namespace Entities.Concrete
{
    [Table("Category", Schema = "dbo")]
    public class Category : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long ModifiedUserId { get; set; }
    }
}
