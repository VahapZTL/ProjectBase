using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.CrossCuttingConcerns.Logging;
using Core.Utilities.Interceptors;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog.Fluent;

namespace Core.Aspects.Autofac.Exception
{
    public class ExceptionNLogAspect : MethodInterception
    {
        private readonly ILogger logger;

        public ExceptionNLogAspect()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            logger.Error(GetLogDetail(invocation, "Exception", e));
        }

        private string GetLogDetail(IInvocation invocation, string type, System.Exception e)
        {
            StringBuilder logDetail = new StringBuilder();
            StringBuilder parameters = new StringBuilder();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                string paramName = invocation.GetConcreteMethod().GetParameters()[i].Name;

                parameters.Append($"{JsonConvert.SerializeObject(invocation.Arguments[i])}");
            }
            logDetail.Append($"[{invocation.Method.Name}] - [{type}:] {parameters.ToString()} - {e.Message}");

            return logDetail.ToString();
        }
    }
}
