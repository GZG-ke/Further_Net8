using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Further_Net8_Repository.Base;
using Further_Net8_Repository.UnitOfWorks;
using Further_Net8_Servive.Base;

namespace Further_Net8_Extensions.ServiceExtensions
{
    public class AutofacModuleRegister : Autofac.Module
    {
        /*
        1、看是哪个容器起的作用，报错是什么
        2、三步走导入autofac容器
        3、生命周期，hashcode对比，为什么controller里没变化
        4、属性注入
        */

        protected override void Load(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;

            var servicesDllFile = Path.Combine(basePath, "Further_Net8_Servive.dll");
            var repositoryDllFile = Path.Combine(basePath, "Further_Net8_Repository.dll");

            var aopTypes = new List<Type>() { typeof(ServiceAOP), typeof(TranAOP) };
            builder.RegisterType<ServiceAOP>();
            builder.RegisterType<TranAOP>();

            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>))
                .InstancePerDependency(); //注册仓储
            builder.RegisterGeneric(typeof(BaseServices<,>)).As(typeof(IBaseServices<,>))
                .EnableInterfaceInterceptors()
                .InterceptedBy(aopTypes.ToArray())
                .InstancePerDependency(); //注册服务

            // 获取 Service.dll 程序集服务，并注册
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired()
                .EnableInterfaceInterceptors()
                .InterceptedBy(aopTypes.ToArray());

            // 获取 Repository.dll 程序集服务，并注册
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency();

            builder.RegisterType<UnitOfWorkManage>().As<IUnitOfWorkManage>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();
        }
    }
}