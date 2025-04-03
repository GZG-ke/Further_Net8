using Further_Net8_Model.Models;
using Further_Net8_Servive.Base;

namespace Further_Net8_Servive.Roles
{
    public interface IRoleServices : IBaseServices<Role>
    {
        Task<Role> SaveRole(string roleName);

        Task<string> GetRoleNameByRid(int rid);
    }
}