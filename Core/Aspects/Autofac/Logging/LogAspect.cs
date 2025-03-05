using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;
        private readonly List<string> _sensitiveParameters = new List<string> { "password", "creditCardNumber", "sifre", "pass", "parola" };

        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType!=typeof(LoggerServiceBase))
            {
                throw new System.Exception(AspectMessages.WrongLoggerType);
            }

            _loggerServiceBase = (LoggerServiceBase) Activator.CreateInstance(loggerService);
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase.Info(GetLogDetail(invocation, "Request"));
        }

        protected override void OnAfter(IInvocation invocation)
        {
            _loggerServiceBase.Info(GetLogDetail(invocation, "Response", invocation.ReturnValue));
        }

        private LogDetail GetLogDetail(IInvocation invocation, string type, object returnValue = null)
        {
            var logParameters = new List<LogParameter>();
            if (type.Equals("Request"))
            {
                for (int i = 0; i < invocation.Arguments.Length; i++)
                {
                    string paramName = invocation.GetConcreteMethod().GetParameters()[i].Name;

                    logParameters.Add(new LogParameter
                    {
                        Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                        Value = MaskSensitiveData(paramName, invocation.Arguments[i]),
                        Type = invocation.Arguments[i].GetType().Name
                    });
                }
            }

            if (type.Equals("Response"))
            {
                logParameters.Add(new LogParameter
                {
                    Name = "ReturnValue",
                    Value = returnValue == null ? "" : returnValue,
                    Type = returnValue?.GetType().Name ?? ""
                });
            }

            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name + type,
                ThreadId = Thread.CurrentThread.ManagedThreadId,
                DateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"),
                LogParameters = logParameters
            };

            return logDetail;
        }

        private object MaskSensitiveData(string paramName, object paramValue)
        {
            if (_sensitiveParameters.Contains(paramName.ToLower()))
            {
                return "******";
            }

            return paramValue;
        }
    }
}
