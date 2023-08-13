using Microsoft.IdentityModel.Tokens;

namespace AuthApi.JWT
{
    public class SigningCredentialHelper
    {
        public static SigningCredentials GetSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
