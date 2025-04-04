﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Further_Net8_Common.Extensions
{
    public static class HttpRequestExtension
    {
        public static string GetRequestBody(this HttpRequest request)
        {
            if (!request.Body.CanRead)
            {
                return default;
            }

            if (!request.Body.CanSeek)
            {
                return default;
            }

            if (request.Body.Length < 1)
            {
                return default;
            }

            var bodyStr = "";
            // 启用倒带功能，就可以让 Request.Body 可以再次读取
            request.Body.Seek(0, SeekOrigin.Begin);
            using (StreamReader reader
                   = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = reader.ReadToEnd();
            }

            request.Body.Position = 0;
            return bodyStr;
        }
    }
}
