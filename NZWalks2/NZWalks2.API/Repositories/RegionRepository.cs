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

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalks2DBContext.AddAsync(region);
            await nZWalks2DBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid Id)
        {
            var region = await nZWalks2DBContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);

            if (region == null) 
            {
                return null;
            }

            // Delete region
            nZWalks2DBContext.Regions.Remove(region);
            await nZWalks2DBContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetallAsync()
        {
           return await nZWalks2DBContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid Id)
        {
            return await nZWalks2DBContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
                       
        }

        public async Task<Region> UpdateAsync(Guid Id, Region region)
        {
            var existingRegion = await nZWalks2DBContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingRegion == null) 
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await nZWalks2DBContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
