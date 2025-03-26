using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace Further_Net8.Extensions
{
    public class AutofacPropertityModuleReg : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var controllerBaseType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
        }
    }
}