using Further_Net8_Common.LogHelper;
using Serilog;
using SqlSugar;

namespace Further_Net8_Common.DB
{
    public static class SqlSugarAop
    {
        public static void OnLogExecuting(ISqlSugarClient sqlSugarScopeProvider, string user, string table, string operate, string sql,
            SugarParameter[] p, ConnectionConfig config)
        {
            try
            {
                //MiniProfiler.Current.CustomTiming($"ConnId:[{config.ConfigId}] SQL：", GetParas(p) + "【SQL语句】：" + sql);

                if (!AppSettings.app(new string[] { "AppSettings", "SqlAOP", "Enabled" }).ObjToBool()) return;

                if (AppSettings.app(new string[] { "AppSettings", "SqlAOP", "LogToConsole", "Enabled" }).ObjToBool() ||
                    AppSettings.app(new string[] { "AppSettings", "SqlAOP", "LogToFile", "Enabled" }).ObjToBool() ||
                    AppSettings.app(new string[] { "AppSettings", "SqlAOP", "LogToDB", "Enabled" }).ObjToBool())
                {
                    using (LogContextExtension.Create.SqlAopPushProperty(sqlSugarScopeProvider))
                    {
                        Log.Information(
                            "------------------ \r\n User:[{User}]  Table:[{Table}]  Operate:[{Operate}] ConnId:[{ConnId}]【SQL语句】: \r\n {Sql}",
                            user, table, operate, config.ConfigId, UtilMethods.GetNativeSql(sql, p));
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Error occured OnLogExcuting:" + e);
            }
        }
    }
}