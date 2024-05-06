using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace Store.Domain.Tools.Security
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJwtThings(this IServiceCollection services,IConfiguration configuration)
        {
            var authOptions = configuration.GetSection("Jwt").Get<AuthOptions>();
            SymmetricSecurityKey key = new SymmetricSecurityKey(AuthSecurity.StringToByte(authOptions.SecretKey));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = authOptions.ValidateIssuer,
                            ValidateAudience = authOptions.ValidateAudience,
                            ValidateLifetime = authOptions.ValidateLifetime,
                            ValidateIssuerSigningKey = authOptions.ValidateIssuerSigningKey,
                            ValidIssuer = authOptions.Issuer,
                            ValidAudience = authOptions.Audience,
                            IssuerSigningKey = key
                        };
                    });
            return services;
        }
    }
}
