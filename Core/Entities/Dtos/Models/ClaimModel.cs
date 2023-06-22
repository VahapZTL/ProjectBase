using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos.Models
{
    public class ClaimModel : IDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
