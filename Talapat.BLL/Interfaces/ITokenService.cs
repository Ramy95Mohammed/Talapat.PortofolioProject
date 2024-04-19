using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.DAL.Entities.IdentityEntities;

namespace Talapat.BLL.Interfaces
{
    public interface ITokenService 
    {
        public Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager);
    }
}
