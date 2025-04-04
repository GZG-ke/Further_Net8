﻿using Further_Net8_Common.Core;
using Further_Net8_Common.Option;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Further_Net8_Extensions.ServiceExtensions
{
    public static class DataProtectionSetup
    {
        public static void AddDataProtectionSetup(this IServiceCollection services)
        {
            var builder = services.AddDataProtection();

            var redisOption = App.GetOptions<RedisOptions>();
            if (redisOption.Enable)
            {
                builder.PersistKeysToStackExchangeRedis(App.GetService<IConnectionMultiplexer>());
                return;
            }

            //默认写到 webroot/temp/
            builder.PersistKeysToFileSystem(new DirectoryInfo(App.WebHostEnvironment.WebRootPath + "/Temp/Sessions/"));
        }
    }
}
