﻿using AutoMapper;

namespace Further_Net8_Extensions.ServiceExtensions
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            //CreateMap<Role, RoleVo>()
            //    .ForMember(a => a.RoleName, o => o.MapFrom(d => d.Name));
            //CreateMap<RoleVo, Role>()
            //    .ForMember(a => a.Name, o => o.MapFrom(d => d.RoleName));

            //CreateMap<SysUserInfo, UserVo>()
            //    .ForMember(a => a.UserName, o => o.MapFrom(d => d.Name));
            //CreateMap<UserVo, SysUserInfo>()
            //    .ForMember(a => a.Name, o => o.MapFrom(d => d.UserName));

            //CreateMap<AuditSqlLog, AuditSqlLogVo>();
            //CreateMap<AuditSqlLogVo, AuditSqlLog>();

            //CreateMap<BusinessTable, BusinessTableVo>();
            //CreateMap<BusinessTableVo, BusinessTable>();
            //CreateMap<MultiBusinessTable, MultiBusinessTableVo>();
            //CreateMap<MultiBusinessTableVo, MultiBusinessTable>();

            //CreateMap<SubLibraryBusinessTable, SubLibraryBusinessTableVo>();
            //CreateMap<SubLibraryBusinessTableVo, SubLibraryBusinessTable>();

            //CreateMap<SysTenant, SysTenantVo>();
            //CreateMap<SysTenantVo, SysTenant>();
        }
    }
}