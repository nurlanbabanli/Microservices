using AuthApi.Entities;

namespace AuthApi.JWT
{
    public interface ITokenHelper
    {
        string CreateToken(User user, List<OperationClaim> claims);
    }
}
