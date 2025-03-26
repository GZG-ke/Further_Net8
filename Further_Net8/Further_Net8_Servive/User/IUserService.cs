using Further_Net8_Model.Models;
using Further_Net8_Model.Vo;
using Further_Net8_Servive.Base;

namespace Further_Net8_Servive.User
{
    public interface IUserService : IBaseServices<SysUserInfo, UserVo>
    {
        Task<string> GetUserRoleNameStr(string loginName, string loginPwd);

        Task<List<RoleModulePermission>> RoleModuleMaps();

        Task<bool> TestTranPropagation();
    }
}