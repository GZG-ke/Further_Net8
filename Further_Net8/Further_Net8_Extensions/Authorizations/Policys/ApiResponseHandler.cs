using System.Security.Claims;
using System.Text.Encodings.Web;
using Further_Net8_Common.HttpContextUser;
using Further_Net8_Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Further_Net8_Extensions.AuthHelper
{
    public class ApiResponseHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAspNetUser _user;

        public ApiResponseHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IAspNetUser user) : base(options, logger, encoder, clock)
        {
            _user = user;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //// 可以查询数据库等操作
            //// 获取当前用户不能放到token中的私密信息
            //var userPhone = "15010000000";

            //var claims = new List<Claim>()
            //{
            //    new Claim("user-phone", userPhone),
            //    new Claim("gw-sign", "gw")
            //};

            //var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, Scheme.Name));
            //var ticket = new AuthenticationTicket(principal, Scheme.Name);
            //await Task.CompletedTask;
            //return AuthenticateResult.Success(ticket);
            throw new Exception();
        }

        //protected override async Task InitializeEventsAsync()
        //{
        //    Response.ContentType = "application/json";
        //    Response.StatusCode = StatusCodes.Status200OK;
        //    Response.HttpContext.Request.Headers.Add("Access-Control-Allow-Origin", "*");
        //    //await Response.WriteAsync(JsonConvert.SerializeObject((new ApiResponse(StatusCode.CODE200)).MessageModel));
        //}

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.ContentType = "application/json";
            Response.StatusCode = StatusCodes.Status401Unauthorized;
            await Response.WriteAsync(JsonConvert.SerializeObject((new ApiResponse(StatusCode.CODE401)).MessageModel));
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.ContentType = "application/json";
            if (_user.MessageModel != null)
            {
                Response.StatusCode = _user.MessageModel.status;
                await Response.WriteAsync(JsonConvert.SerializeObject(_user.MessageModel));
            }
            else
            {
                Response.StatusCode = StatusCodes.Status403Forbidden;
                await Response.WriteAsync(JsonConvert.SerializeObject((new ApiResponse(StatusCode.CODE403)).MessageModel));
            }
        }
    }
}