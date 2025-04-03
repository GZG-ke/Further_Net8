using Further_Net8_Model.Models;
using Further_Net8_Servive.Base;

namespace Further_Net8_Servive.User
{
    public interface IUserRoleServices : IBaseServices<UserRole>
    {
        Task<UserRole> SaveUserRole(long uid, long rid);

        Task<int> GetRoleIdByUid(long uid);
    }
}