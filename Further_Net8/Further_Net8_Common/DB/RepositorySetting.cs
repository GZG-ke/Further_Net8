using Further_Net8_Common.Core;
using Further_Net8_Model.Models.RootTkey;
using Further_Net8_Model.Tenants;
using SqlSugar;

namespace Further_Net8_Common.DB
{
    public class RepositorySetting
    {
        /// <summary>
        /// 配置租户
        /// </summary>
        public static void SetTenantEntityFilter(SqlSugarScopeProvider db)
        {
            if (App.User is not { ID: > 0, TenantId: > 0 })
            {
                return;
            }

            //多租户 单表字段
            db.QueryFilter.AddTableFilter<ITenantEntity>(it => it.TenantId == App.User.TenantId || it.TenantId == 0);

            //多租户 多表
            db.SetTenantTable(App.User.TenantId.ToString());
        }

        private static readonly Lazy<IEnumerable<Type>> AllEntitys = new(() =>
        {
            return typeof(RootEntityTkey<>).Assembly
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(it => it.FullName != null && it.FullName.StartsWith("BCVP.Net8.Model"));
        });

        public static IEnumerable<Type> Entitys => AllEntitys.Value;

        /// <summary>
        /// 配置实体软删除过滤器<br/>
        /// 统一过滤 软删除 无需自己写条件
        /// </summary>
        public static void SetDeletedEntityFilter(SqlSugarScopeProvider db)
        {
            db.QueryFilter.AddTableFilter<IDeleteFilter>(it => it.IsDeleted == false);
        }
    }
}