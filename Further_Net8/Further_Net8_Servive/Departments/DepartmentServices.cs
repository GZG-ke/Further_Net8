using AutoMapper;
using Further_Net8_Common;
using Further_Net8_Common.Attributes;
using Further_Net8_Model.Models;
using Further_Net8_Model.Vo;
using Further_Net8_Repository.Base;
using Further_Net8_Servive.Base;

namespace Further_Net8_Servive.Departments
{
    public class DepartmentServices : BaseServices<Department, UserVo>, IDepartmentServices
    {
        private readonly IBaseRepository<Department> _dal;

        public DepartmentServices(IMapper mapper, IBaseRepository<Department> baseRepository) : base(mapper, baseRepository)
        {
            _dal = baseRepository;
        }

        /// <summary>
        /// 测试使用同事务
        /// </summary>
        /// <returns></returns>
        [UseTran(Propagation = Propagation.Required)]
        public async Task<bool> TestTranPropagation2()
        {
            TimeSpan timeSpan = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var id = timeSpan.TotalSeconds.ObjToLong();
            var insertDepartment = await _dal.Add(new Department()
            {
                Id = id,
                Name = $"department name {id}",
                CodeRelationship = "",
                OrderSort = 0,
                Status = true,
                IsDeleted = false,
                Pid = 0
            });

            await Console.Out.WriteLineAsync($"db context id : {base.Db.ContextID}");

            return true;
        }
    }
}