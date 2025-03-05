﻿using Core.Utilities.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ErrorDetails : ErrorResult
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
