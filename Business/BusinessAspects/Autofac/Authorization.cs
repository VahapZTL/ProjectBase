using Business.Constants;
using Castle.DynamicProxy;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac
{
    public class Authorization : MethodInterception
    {
        private List<EnumUserType> roles;
        //private string[] roles;
        private IHttpContextAccessor httpContextAccessor;

        public Authorization()
        {
            roles = new List<EnumUserType>();
            roles.Add(EnumUserType.SystemAdministrator);
            httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        //public Authorization(string roles) : base()
        //{
        //    this.roles = roles.Split(',');
        //}

        public Authorization(params EnumUserType[] roles) : base()
        {
            this.roles?.AddRange(roles);
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = httpContextAccessor.HttpContext.User.ClaimRoles();

            EnumUserType userType;

            foreach (var roleClaim in roleClaims) 
            {
                bool success = Enum.TryParse(roleClaim, out userType);
                if (success && roleClaims.Contains(userType.ToString()))
                    return;
            }
            
            throw new UnauthorizedAccessException(Messages.AuthorizationDenied);
        }

        //protected override void OnBefore(IInvocation invocation)
        //{
        //    var roleClaims = httpContextAccessor.HttpContext.User.ClaimRoles();
        //    foreach (var role in roles)
        //    {
        //        if (roleClaims.Contains(role))
        //        {
        //            return;
        //        }
        //    }
        //    throw new UnauthorizedAccessException(Messages.AuthorizationDenied);
        //}
    }
}
