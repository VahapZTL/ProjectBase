using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos.Request
{
    public class RequestValidateToken : IDto
    {
        public string Token { get; set; }
    }
}
