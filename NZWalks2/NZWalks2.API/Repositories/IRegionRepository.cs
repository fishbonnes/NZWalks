using NZWalks2.API.Models.Domain;

namespace NZWalks2.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetallAsync();

        Task<Region> GetAsync(Guid Id);

        Task<Region> AddAsync(Region region);

        Task<Region> DeleteAsync(Guid Id);

        Task<Region> UpdateAsync(Guid Id, Region region);
    }
}
