﻿using Further_Net8_Common;
using Further_Net8_Common.Core;
using Further_Net8_Common.Helper;
using Further_Net8_Common.Helper.Logs;
using Further_Net8_Common.Option;
using Further_Net8_Common.Serilog.Configuration;
using Further_Net8_Common.Serilog.Extensions;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;

namespace Further_Net8_Extensions.ServiceExtensions
{
    public static class SerilogSetup
    {
        public static IHostBuilder AddSerilogSetup(this IHostBuilder host)
        {
            if (host == null) throw new ArgumentNullException(nameof(host));

            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(AppSettings.Configuration)
                .Enrich.FromLogContext()
                //输出到控制台
                .WriteToConsole()
                //将日志保存到文件中
                .WriteToFile()
                //配置日志库
                .WriteToLogBatching();

            var option = App.GetOptions<SeqOptions>();
            //配置Seq日志中心
            if (option.Enabled)
            {
                var address = option.Address;
                var apiKey = option.ApiKey;
                if (!address.IsNullOrEmpty())
                {
                    loggerConfiguration =
                        loggerConfiguration.WriteTo.Seq(address, restrictedToMinimumLevel: LogEventLevel.Verbose,
                            apiKey: apiKey, eventBodyLimitBytes: 10485760);
                }
            }

            Log.Logger = loggerConfiguration.CreateLogger();

            //Serilog 内部日志
            var file = File.CreateText(LogContextStatic.Combine($"SerilogDebug{DateTime.Now:yyyyMMdd}.txt"));
            SelfLog.Enable(TextWriter.Synchronized(file));

            host.UseSerilog();
            return host;
        }
    }
}