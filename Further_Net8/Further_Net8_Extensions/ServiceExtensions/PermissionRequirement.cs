using Microsoft.AspNetCore.Authorization;

namespace Further_Net8_Extensions.ServiceExtensions
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public List<PermissionItem> Permissions { get; set; } = new List<PermissionItem>();
    }
}