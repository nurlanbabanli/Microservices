using AuthApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthApi.JWT
{
    public class JwtHelper:ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;
        public JwtHelper(IConfiguration configuration)
        {
            _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public string CreateToken(User user, List<OperationClaim> claims)
        {
            if (_tokenOptions==null) return null;

            var tokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialHelper.GetSigningCredentials(securityKey);
            var jwtSecurityToken = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, claims);
            var token = (new JwtSecurityTokenHandler()).WriteToken(jwtSecurityToken);

            return token;
        }

        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            return new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration),
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
                );
        }

        private List<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("firstName", user.FirstName));
            claims.Add(new Claim("lastName", user.LastName));
            operationClaims.Select(c => c.Name).ToList().ForEach(x => claims.Add(new Claim("roles", x)));
            return claims;
        }
    }
}
