using Further_Net8_Common;
using Further_Net8_Common.Helper;
using Further_Net8_Model.Models;
using Further_Net8_Servive.Base;

namespace Further_Net8_Servive.User
{
    public class UserRoleServices : BaseServices<UserRole>, IUserRoleServices
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        public async Task<UserRole> SaveUserRole(long uid, long rid)
        {
            UserRole userRole = new UserRole(uid, rid);

            UserRole model = new UserRole();
            var userList = await base.Query(a => a.UserId == userRole.UserId && a.RoleId == userRole.RoleId);
            if (userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await base.Add(userRole);
                model = await base.QueryById(id);
            }

            return model;
        }

        //[Caching(AbsoluteExpiration = 30)]
        public async Task<int> GetRoleIdByUid(long uid)
        {
            return ((await base.Query(d => d.UserId == uid)).OrderByDescending(d => d.Id).LastOrDefault()?.RoleId).ObjToInt();
        }
    }
}