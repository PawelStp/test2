using System.Linq;
using System.Security.Claims;

namespace Games.Api.Authentication
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

            return long.Parse(userId);
        }
    }
}
