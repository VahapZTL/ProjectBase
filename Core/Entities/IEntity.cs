using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public interface IEntity
    {
        int StatusId { get; set; }
        DateTime CreatedDate { get; set; }
        long CreatedUserId { get; set; }
        DateTime ModifiedDate { get; set; }
        long ModifiedUserId { get; set; }
    }
}
