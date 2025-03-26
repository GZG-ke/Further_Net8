﻿using System.Security.Claims;

namespace Further_Net8_Common.HttpContextUser
{
    public interface IAspNetUser
    {
        string Name { get; }
        long ID { get; }
        long TenantId { get; }

        bool IsAuthenticated();

        IEnumerable<Claim> GetClaimsIdentity();

        List<string> GetClaimValueByType(string ClaimType);

        string GetToken();

        List<string> GetUserInfoFromToken(string ClaimType);
    }
}