﻿using Further_Net8_Model.Models;
using Further_Net8_Repository.Base;
using Further_Net8_Repository.UnitOfWorks;
using SqlSugar;

namespace Further_Net8_Repository
{
    public class RoleModulePermissionRepository : BaseRepository<RoleModulePermission>, IRoleModulePermissionRepository
    {
        public RoleModulePermissionRepository(IUnitOfWorkManage unitOfWorkManage) : base(unitOfWorkManage)
        {
        }

        public async Task<List<TestMuchTableResult>> QueryMuchTable()
        {
            return await QueryMuch<RoleModulePermission, Modules, Permission, TestMuchTableResult>(
                (rmp, m, p) => new object[] {
                    JoinType.Left, rmp.ModuleId == m.Id,
                    JoinType.Left,  rmp.PermissionId == p.Id
                },

                (rmp, m, p) => new TestMuchTableResult()
                {
                    moduleName = m.Name,
                    permName = p.Name,
                    rid = rmp.RoleId,
                    mid = rmp.ModuleId,
                    pid = rmp.PermissionId
                },

                (rmp, m, p) => rmp.IsDeleted == false
                );
        }

        /// <summary>
        /// 角色权限Map
        /// RoleModulePermission, Module, Role 三表联合
        /// 第四个类型 RoleModulePermission 是返回值
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleModulePermission>> RoleModuleMaps()
        {
            return await QueryMuch<RoleModulePermission, Modules, Role, RoleModulePermission>(
                (rmp, m, r) => new object[] {
                    JoinType.Left, rmp.ModuleId == m.Id,
                    JoinType.Left,  rmp.RoleId == r.Id
                },

                (rmp, m, r) => new RoleModulePermission()
                {
                    Role = r,
                    Module = m,
                    IsDeleted = rmp.IsDeleted
                },

                (rmp, m, r) => rmp.IsDeleted == false && m.IsDeleted == false && r.IsDeleted == false
                );
        }

        /// <summary>
        /// 查询出角色-菜单-接口关系表全部Map属性数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleModulePermission>> GetRMPMaps()
        {
            return await Db.Queryable<RoleModulePermission>()
                .Mapper(rmp => rmp.Module, rmp => rmp.ModuleId)
                .Mapper(rmp => rmp.Permission, rmp => rmp.PermissionId)
                .Mapper(rmp => rmp.Role, rmp => rmp.RoleId)
                .Where(d => d.IsDeleted == false)
                .ToListAsync();
        }

        /// <summary>
        /// 查询出角色-菜单-接口关系表全部Map属性数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleModulePermission>> GetRMPMapsPage()
        {
            return await Db.Queryable<RoleModulePermission>()
                .Mapper(rmp => rmp.Module, rmp => rmp.ModuleId)
                .Mapper(rmp => rmp.Permission, rmp => rmp.PermissionId)
                .Mapper(rmp => rmp.Role, rmp => rmp.RoleId)
                .ToPageListAsync(1, 5, 10);
        }

        /// <summary>
        /// 批量更新菜单与接口的关系
        /// </summary>
        /// <param name="permissionId">菜单主键</param>
        /// <param name="moduleId">接口主键</param>
        /// <returns></returns>
        public async Task UpdateModuleId(long permissionId, long moduleId)
        {
            await Db.Updateable<RoleModulePermission>(it => it.ModuleId == moduleId).Where(
                it => it.PermissionId == permissionId).ExecuteCommandAsync();
        }
    }
}