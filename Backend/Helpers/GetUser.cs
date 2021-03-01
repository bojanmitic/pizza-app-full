using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace pizza_server.Helpers
{
    public class GetUser
    {
        public static int GetUserId(IHttpContextAccessor httpContextAccessor) => 
            int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public static string GetUserRole(IHttpContextAccessor httpContextAccessor) =>
            httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
    }
}
