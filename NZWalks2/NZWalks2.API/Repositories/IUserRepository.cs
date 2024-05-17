using Microsoft.IdentityModel.Tokens;
using NZWalks2.API.Models.Domain;

namespace NZWalks2.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
