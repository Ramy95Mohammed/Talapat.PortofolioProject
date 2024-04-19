using Microsoft.AspNetCore.Identity;
using Talapat.DAL.Entities.IdentityEntities;

namespace Talapat.DAL.IdentityDbContext
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userMaanger)
        {
                        
            if(!userMaanger.Users.Any())
            {
                var user = new AppUser() {
                    DisplayName = "Ramy Mohammed",
                    UserName = "Ramy_Mohammed",
                    Email = "Ramy.Abd.Almksoud.95@gmail.com",
                    PhoneNumber = "01024579669",
                    Address = new Address() { 
                     FirstName="Ramy",
                     LastName="Hashad",
                     Country="Menofia",
                     Street="Copry Mobarak"   ,
                     City="Sheebien Alkoom"
                    }
                };
                await userMaanger.CreateAsync(user, "Pa$$W0rd");
            }
        }
    }
}
