using Microsoft.EntityFrameworkCore;
using NZWalks2.API.Data;
using NZWalks2.API.Models.Domain;

namespace NZWalks2.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalks2DBContext nZWalks2DBContext;

        public RegionRepository(NZWalks2DBContext nZWalks2DBContext)
        {
            this.nZWalks2DBContext = nZWalks2DBContext;
        }
        public async Task<IEnumerable<Region>> GetallAsync()
        {
           return await nZWalks2DBContext.Regions.ToListAsync();
        }
    }
}
