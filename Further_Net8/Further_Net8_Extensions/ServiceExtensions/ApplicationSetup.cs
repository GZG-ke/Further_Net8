﻿using Further_Net8_Common.Core;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Further_Net8_Extensions.ServiceExtensions
{
    public static class ApplicationSetup
    {
        public static void UseApplicationSetup(this WebApplication app)
        {
            app.Lifetime.ApplicationStarted.Register(() =>
            {
                App.IsRun = true;
            });

            app.Lifetime.ApplicationStopped.Register(() =>
            {
                App.IsRun = false;

                //清除日志
                Log.CloseAndFlush();
            });
        }
    }
}