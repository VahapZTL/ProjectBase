using Castle.DynamicProxy;
using Core.Entities.Dtos.Request;
using Core.Extensions;
using Core.Utilities.Helper;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Auth
{
    public class Authorization : MethodInterception
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public Authorization()
        {
            httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var accessToken = httpContextAccessor?.HttpContext?.Session.Get<AccessToken>("Authorization");

            RequestValidateToken request = new RequestValidateToken();

            request.Token = accessToken.Token;

            var response = Utility.PostMethod<RequestValidateToken, SuccessResult>(ConfigHelper.GetConfig("ApiAuthUrl"), "ValidateToken", request);

            if (!response.Success)
            {
                httpContextAccessor?.HttpContext.Session.Remove("Authorization");
                httpContextAccessor?.HttpContext.Response.Redirect(ConfigHelper.GetConfig("LoginPage"));
            }  
        }
    }
}
