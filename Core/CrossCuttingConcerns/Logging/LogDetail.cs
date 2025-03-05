using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string MethodName { get; set; }
        public int ThreadId { get; set; }
        public string DateTime { get; set; }
        public List<LogParameter> LogParameters { get; set; }
    }
}
