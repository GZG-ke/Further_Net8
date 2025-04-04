﻿using Further_Net8_Common;
using Further_Net8_Common.Helper;
using Further_Net8_Common.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Further_Net8_Extensions.HostedService
{
    public sealed class SeedDataHostedService : IHostedService
    {
        private readonly MyContext _myContext;
        private readonly ILogger<SeedDataHostedService> _logger;
        private readonly string _webRootPath;

        public SeedDataHostedService(
            MyContext myContext,
            IWebHostEnvironment webHostEnvironment,
            ILogger<SeedDataHostedService> logger)
        {
            _myContext = myContext;
            _logger = logger;
            _webRootPath = webHostEnvironment.WebRootPath;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start Initialization Db Seed Service!");
            await DoWork();
        }

        private async Task DoWork()
        {
            try
            {
                if (AppSettings.app("AppSettings", "SeedDBEnabled").ObjToBool() || AppSettings.app("AppSettings", "SeedDBDataEnabled").ObjToBool())
                {
                    await DBSeed.SeedAsync(_myContext, _webRootPath);

                    //日志
                    DBSeed.MigrationLogs(_myContext);

                    //多租户 同步
                    await DBSeed.TenantSeedAsync(_myContext);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured seeding the Database.");
                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stop Initialization Db Seed Service!");
            return Task.CompletedTask;
        }
    }
}