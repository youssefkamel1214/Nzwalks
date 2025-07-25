using Microsoft.AspNetCore.Identity;

namespace Nzwalks_api.Repostories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<String> roles);
    }
}
