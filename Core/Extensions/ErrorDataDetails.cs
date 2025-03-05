using Core.Utilities.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ErrorDataDetails<T> : ErrorDataResult<T>
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
