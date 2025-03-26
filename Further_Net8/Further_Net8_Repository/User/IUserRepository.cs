using Further_Net8_Model.Models;

namespace Further_Net8_Repository.User
{
    public interface IUserRepository
    {
        Task<List<SysUserInfo>> Query();

        Task<List<RoleModulePermission>> RoleModuleMaps();
    }
}