using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductBusiness>().As<IProductBusiness>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();

            builder.RegisterType<CategoryBusiness>().As<ICategoryBusiness>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();

            builder.RegisterType<UserBusiness>().As<IUserBusiness>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            builder.RegisterType<DescriptionBusiness>().As<IDescriptionBusiness>(); 
            builder.RegisterType<DescriptionRepository>().As<IDescriptionRepository>();

            builder.RegisterType<LanguageBusiness>().As<ILanguageBusiness>();
            builder.RegisterType<LanguageRepository>().As<ILanguageRepository>();

            builder.RegisterType<ResetPasswordRequestBusiness>().As<IResetPasswordRequestBusiness>();
            builder.RegisterType<ResetPasswordRequestsRepository>().As<IResetPasswordRequestsRepository>();

            builder.RegisterType<RolePermissionsBusiness>().As<IRolePermissionsBusiness>().EnableInterfaceInterceptors().EnableClassInterceptors();
            builder.RegisterType<RolePermissionsRepository>().As<IRolePermissionsRepository>();

            builder.RegisterType<RolePermissionsMatchBusiness>().As<IRolePermissionsMatchBusiness>();
            builder.RegisterType<RolePermissionsMatchRepository>().As<IRolePermissionsMatchRepository>();

            builder.RegisterType<RolePermissionsTypeBusiness>().As<IRolePermissionsTypeBusiness>();
            builder.RegisterType<RolePermissionsTypeRepository>().As<IRolePermissionsTypeRepository>();

            builder.RegisterType<UserTypeBusiness>().As<IUserTypeBusiness>();
            builder.RegisterType<UserTypeRepository>().As<IUserTypeRepository>();

            builder.RegisterType<AuthBusiness>().As<IAuthBusiness>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
