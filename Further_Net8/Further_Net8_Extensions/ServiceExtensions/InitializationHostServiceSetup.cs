using Further_Net8_Extensions.HostedService;
using Microsoft.Extensions.DependencyInjection;

namespace Further_Net8_Extensions.ServiceExtensions
{
    public static class InitializationHostServiceSetup
    {
        /// <summary>
        /// 应用初始化服务注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddInitializationHostServiceSetup(this IServiceCollection services)
        {
            if (services is null)
            {
                ArgumentNullException.ThrowIfNull(nameof(services));
            }
            services.AddHostedService<SeedDataHostedService>();
            services.AddHostedService<EventBusHostedService>();
        }
    }
}