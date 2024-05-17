using NZWalks2.API.Models.Domain;

namespace NZWalks2.API.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
