using ApiGateway.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ApiGateway.Extensions
{
    public static class JwtExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var key = JwtBearerDefaults.AuthenticationScheme;

            var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(key, opt =>
            {
                opt.TokenValidationParameters=new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer=tokenOptions.Issuer,
                    ValidAudience=tokenOptions.Audience,
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                    ValidateLifetime = true,
                    ClockSkew=TimeSpan.Zero,
                };
            });
        }
    }
}
