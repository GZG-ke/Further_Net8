using Further_Net8_Common.HttpContextUser;
using Further_Net8_Servive.User;
using Microsoft.AspNetCore.Mvc;

namespace Further_Net8.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IAspNetUser _user;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger,
            IAspNetUser user,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService)
        {
            _logger = logger;
            _user = user;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public string Get(int page = 1, string key = "")
        {
            long iD = _user.ID;
            _logger.LogInformation(key, page);
            return "OK!!!";
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<string> Get(string id)
        {
            await _userService.TestTranPropagation();
            _logger.LogError("test wrong");
            return "value";
        }
    }
}