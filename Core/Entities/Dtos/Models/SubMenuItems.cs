using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos.Models
{
    public class SubMenuItems : IDto
    {
        public string MenuName { get; set; }
        public string MenuIconClass { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public long Priorty { get; set; }
        public IList<string> Actions { get; set; }
    }
}
