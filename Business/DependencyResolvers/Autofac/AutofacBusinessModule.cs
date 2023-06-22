using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
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
            builder.RegisterType<EfProductDal>().As<IProductDal>();

            builder.RegisterType<CategoryBusiness>().As<ICategoryBusiness>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<UserBusiness>().As<IUserBusiness>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<DescriptionBusiness>().As<IDescriptionBusiness>(); 
            builder.RegisterType<EfDescriptionDal>().As<IDescriptionDal>();

            builder.RegisterType<LanguageBusiness>().As<ILanguageBusiness>();
            builder.RegisterType<EfLanguageDal>().As<ILanguageDal>();

            builder.RegisterType<ResetPasswordRequestBusiness>().As<IResetPasswordRequestBusiness>();
            builder.RegisterType<EfResetPasswordRequestsDal>().As<IResetPasswordRequestsDal>();

            builder.RegisterType<RolePermissionsBusiness>().As<IRolePermissionsBusiness>();
            builder.RegisterType<EfRolePermissionsDal>().As<IRolePermissionsDal>();

            builder.RegisterType<RolePermissionsMatchBusiness>().As<IRolePermissionsMatchBusiness>();
            builder.RegisterType<EfRolePermissionsMatchDal>().As<IRolePermissionsMatchDal>();

            builder.RegisterType<RolePermissionsTypeBusiness>().As<IRolePermissionsTypeBusiness>();
            builder.RegisterType<EfRolePermissionsTypeDal>().As<IRolePermissionsTypeDal>();

            builder.RegisterType<UserTypeBusiness>().As<IUserTypeBusiness>();
            builder.RegisterType<EfUserTypeDal>().As<IUserTypeDal>();

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
