using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Newtonsoft.Json;
using NLog;
using System.Text;

namespace Core.Aspects.Autofac.Logging
{
    public class NLogAspect : MethodInterception
    {
        private readonly ILogger logger;

        public NLogAspect()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            logger.Info(GetLogDetail(invocation, "Request"));
        }

        protected override void OnAfter(IInvocation invocation)
        {
            logger.Info(GetLogDetail(invocation, "Response", invocation.ReturnValue));
        }

        private string GetLogDetail(IInvocation invocation, string type, object returnValue = null)
        {
            StringBuilder logDetail = new StringBuilder();

            if (type.Equals("Request"))
            {
                StringBuilder parameters = new StringBuilder();
                for (int i = 0; i < invocation.Arguments.Length; i++)
                {
                    string paramName = invocation.GetConcreteMethod().GetParameters()[i].Name;

                    parameters.Append($"{JsonConvert.SerializeObject(invocation.Arguments[i])}");
                }
                logDetail.Append($"[{invocation.Method.Name}] - [{type}:] {parameters.ToString()}");
            }

            if (type.Equals("Response"))
                logDetail.Append($"[{invocation.Method.Name}] - [{type}:] {(returnValue == null ? "" : JsonConvert.SerializeObject(returnValue))}");

            return logDetail.ToString();
        }
    }
}
