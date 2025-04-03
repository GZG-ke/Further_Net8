using AutoMapper;
using Further_Net8_Model.Models;
using Further_Net8_Model.ViewModels;

namespace Further_Net8_Extensions.AutoMapper
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<BlogArticle, BlogViewModels>();
            CreateMap<BlogViewModels, BlogArticle>();

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