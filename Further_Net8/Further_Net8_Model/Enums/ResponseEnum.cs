﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Further_Net8_Model.Enums
{
    public enum ResponseEnum
    {
        /// <summary>
        /// 无权限
        /// </summary>
        [Description("无权限")]
        NoPermissions = 401,
        /// <summary>
        /// 找不到指定资源
        /// </summary>
        [Description("找不到指定资源")]
        NoFound = 404,
        /// <summary>
        /// 找不到指定资源
        /// </summary>
        [Description("服务器错误")]
        ServerError = 500
    }
}
