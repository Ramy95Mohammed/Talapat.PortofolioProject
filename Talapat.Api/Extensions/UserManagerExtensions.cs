using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talapat.DAL.Entities.IdentityEntities;

namespace Talapat.Api.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindUserWithAddressByEmailAsync(this UserManager<AppUser> usermanager,ClaimsPrincipal User )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await usermanager.Users.Include(u => u.Address).SingleOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
