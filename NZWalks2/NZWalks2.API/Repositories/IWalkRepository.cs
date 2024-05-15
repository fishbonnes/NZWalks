using NZWalks2.API.Models.Domain;

namespace NZWalks2.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetallAsync();

        Task<Walk> GetAsync(Guid id);

        Task<Walk> AddAsync(Walk walk);

        Task<Walk> DeleteAsync(Guid id);

        Task<Walk> UpdateAsync(Guid id, Walk walk);
    }
}
