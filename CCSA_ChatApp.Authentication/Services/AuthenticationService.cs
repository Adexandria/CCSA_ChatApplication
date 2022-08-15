using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSA_ChatApp.Authentication.Services
{
    public class AuthenticationService
    {
        
        public static void ConfigureServices(IConfiguration config,IServiceCollection service)
        {
            IConfiguration _jwtKey = config.GetSection("JWTSettings");
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
            AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwtKey.GetSection("validIssuer").Value,
                   // AuthenticationType = "Bearer",
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey.GetSection("securityKey").Value))
                };

            });
        }

    }
}
