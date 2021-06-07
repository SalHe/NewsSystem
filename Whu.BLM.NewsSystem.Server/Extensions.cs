using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Whu.BLM.NewsSystem.Server.Constants;
using Whu.BLM.NewsSystem.Shared.Entity.Identity;

namespace Whu.BLM.NewsSystem.Server
{
    public static class Extensions
    {
        public static async Task<User> GetCurrentUser(this HttpContext httpContext, DbSet<User> users)
        {
            var username = httpContext.User.FindFirst(MyClaimTypes.Username)?.Value;
            return await users.FirstAsync(x => x.Username.Equals(username));
        }
    }
}