﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Further_Net8_Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System.Web;
using Newtonsoft.Json;
using Further_Net8_Common.Helper.Logs;
using Further_Net8_Common.Helper;
using Further_Net8_Common.HttpContextUser;

namespace Further_Net8_Extensions.Middlewares
{
    /// <summary>
    /// 中间件
    /// 记录用户方访问数据
    /// </summary>
    public class RecordAccessLogsMiddleware
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly RequestDelegate _next;

        private readonly IAspNetUser _user;
        private readonly ILogger<RecordAccessLogsMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;
        private Stopwatch _stopwatch;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public RecordAccessLogsMiddleware(RequestDelegate next, IAspNetUser user, ILogger<RecordAccessLogsMiddleware> logger,
            IWebHostEnvironment environment)
        {
            _next = next;
            _user = user;
            _logger = logger;
            _environment = environment;
            _stopwatch = new Stopwatch();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (AppSettings.app("Middleware", "RecordAccessLogs", "Enabled").ObjToBool())
            {
                var api = context.Request.Path.ObjToString().TrimEnd('/').ToLower();
                var ignoreApis = AppSettings.app("Middleware", "RecordAccessLogs", "IgnoreApis");

                // 过滤，只有接口
                if (api.Contains("api") && !ignoreApis.Contains(api))
                {
                    _stopwatch.Restart();
                    var userAccessModel = new UserAccessModel();

                    HttpRequest request = context.Request;

                    userAccessModel.API = api;
                    userAccessModel.User = _user.Name;
                    userAccessModel.IP = IpLogMiddleware.GetClientIP(context);
                    userAccessModel.BeginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    userAccessModel.RequestMethod = request.Method;
                    userAccessModel.Agent = request.Headers["User-Agent"].ObjToString();


                    // 获取请求body内容
                    if (request.Method.ToLower().Equals("post") || request.Method.ToLower().Equals("put"))
                    {
                        // 启用倒带功能，就可以让 Request.Body 可以再次读取
                        request.EnableBuffering();

                        Stream stream = request.Body;
                        byte[] buffer = new byte[request.ContentLength.Value];
                        stream.Read(buffer, 0, buffer.Length);
                        userAccessModel.RequestData = Encoding.UTF8.GetString(buffer);

                        request.Body.Position = 0;
                    }
                    else if (request.Method.ToLower().Equals("get") || request.Method.ToLower().Equals("delete"))
                    {
                        userAccessModel.RequestData = HttpUtility.UrlDecode(request.QueryString.ObjToString(), Encoding.UTF8);
                    }

                    await _next(context);

                    // 响应完成记录时间和存入日志
                    context.Response.OnCompleted(() =>
                    {
                        _stopwatch.Stop();

                        userAccessModel.OPTime = _stopwatch.ElapsedMilliseconds + "ms";

                        // 自定义log输出
                        var requestInfo = JsonConvert.SerializeObject(userAccessModel);
                        Parallel.For(0, 1, e =>
                        {
                            //LogLock.OutSql2Log("RecordAccessLogs", new string[] { requestInfo + "," }, false);
                            LogLock.OutLogAOP("RecordAccessLogs", context.TraceIdentifier,
                                new string[] { userAccessModel.GetType().ToString(), requestInfo }, false);
                        });
                        //var logFileName = FileHelper.GetAvailableFileNameWithPrefixOrderSize(_environment.ContentRootPath, "RecordAccessLogs");
                        //SerilogServer.WriteLog(logFileName, new string[] { requestInfo + "," }, false);
                        return Task.CompletedTask;
                    });
                }
                else
                {
                    await _next(context);
                }
            }
            else
            {
                await _next(context);
            }
        }

    }

    public class UserAccessModel
    {
        public string User { get; set; }
        public string IP { get; set; }
        public string API { get; set; }
        public string BeginTime { get; set; }
        public string OPTime { get; set; }
        public string RequestMethod { get; set; }
        public string RequestData { get; set; }
        public string Agent { get; set; }
    }
}
