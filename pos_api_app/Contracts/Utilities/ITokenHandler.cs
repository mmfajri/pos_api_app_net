using System.Security.Claims;

namespace pos_api_app.Contracts.Utilities;

public interface ITokenHandler
{
    string GenerateToken(IEnumerable<Claim> claims);
}
