using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatApp.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetClaimValue(this IEnumerable<Claim> claims,string claimType)
        {
            var value = claims.FirstOrDefault(x => x.Type == claimType).Value;
            return value;
        }
    }
}
