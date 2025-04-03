﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Further_Net8_Common;
using Further_Net8_Common.Helper;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Further_Net8_Extensions.Middlewares
{
    /// <summary>
    /// MiniProfiler性能分析
    /// </summary>
    public static class MiniProfilerMiddleware
    {
        public static void UseMiniProfilerMiddleware(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            try
            {
                if (AppSettings.app("Startup", "MiniProfiler", "Enabled").ObjToBool())
                {
                    // 性能分析
                    app.UseMiniProfiler();

                }
            }
            catch (Exception e)
            {
                Log.Error($"An error was reported when starting the MiniProfilerMildd.\n{e.Message}");
                throw;
            }
        }
    }
}
