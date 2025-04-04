﻿using Further_Net8_Model.Models;
using Further_Net8_Repository.Base;

namespace Further_Net8_Repository
{
    public interface IRoleModulePermissionRepository : IBaseRepository<RoleModulePermission>//类名
    {
        Task<List<TestMuchTableResult>> QueryMuchTable();

        Task<List<RoleModulePermission>> RoleModuleMaps();

        Task<List<RoleModulePermission>> GetRMPMaps();

        /// <summary>
        /// 批量更新菜单与接口的关系
        /// </summary>
        /// <param name="permissionId">菜单主键</param>
        /// <param name="moduleId">接口主键</param>
        /// <returns></returns>
        Task UpdateModuleId(long permissionId, long moduleId);
    }
}