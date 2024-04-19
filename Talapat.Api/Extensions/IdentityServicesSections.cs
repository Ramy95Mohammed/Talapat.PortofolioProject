using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Talapat.BLL.Interfaces;
using Talapat.BLL.Services;
using Talapat.DAL.Entities.IdentityEntities;
using Talapat.DAL.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Talapat.Api.Extensions
{
    public static class IdentityServicesSections
    {
        
        public static IServiceCollection AddIdentityServies(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddIdentity<AppUser, IdentityRole>(options => {
                //Password Configurations

            }).AddEntityFrameworkStores<AppIdentityDbContet>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;

            }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience=true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateIssuer=true,
                    ValidIssuer= configuration["JWT:ValidIssuer"],
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                    ValidateLifetime=true
                };
            });
            return services;
        }
    }
}
