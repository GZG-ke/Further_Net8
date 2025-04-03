using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Further_Net8_Model.Models;
using Further_Net8_Model.Tenants;
using SqlSugar;

namespace Further_Net8_Common.Seed.SeedData
{
    /// <summary>
    /// 租户 种子数据
    /// </summary>
    public class TenantSeedData : IEntitySeedData<SysTenant>
    {
        public IEnumerable<SysTenant> InitSeedData()
        {
            return new[]
            {
            new SysTenant()
            {
                Id = 1000003,
                ConfigId = "FurtherFromDB",
                Name = "王五",
                TenantType = TenantTypeEnum.Db,
                DbType = DbType.SqlServer,
                Connection = "Server=192.168.2.161; Database=FurtherFromDB;User ID=sa;Password=bb123456??;multipleactiveresultsets=True;trustServerCertificate=true",
            }
        };
        }

        public IEnumerable<SysTenant> SeedData()
        {
            return default;
        }

        public Task CustomizeSeedData(ISqlSugarClient db)
        {
            return Task.CompletedTask;
        }
    }
}
