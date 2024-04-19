using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talapat.DAL.Entities.IdentityEntities;

namespace Talapat.DAL.Identity
{
    public class AppIdentityDbContet : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContet(DbContextOptions<AppIdentityDbContet> options):base(options)
        {
                
        }
        
    }
}
