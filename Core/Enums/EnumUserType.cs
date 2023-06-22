using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum EnumUserType : long
    {
        SystemAdministrator = 1,
        LawyerAdministrator,
        Lawyer,
        InternLawyer,
        Client,
        Customer
    }
}
