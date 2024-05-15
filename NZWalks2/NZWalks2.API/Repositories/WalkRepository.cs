using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NZWalks2.API.Data;
using NZWalks2.API.Models.Domain;

namespace NZWalks2.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalks2DBContext nZWalks2DBContext;

        public WalkRepository(NZWalks2DBContext nZWalks2DBContext) 
        {
            this.nZWalks2DBContext = nZWalks2DBContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await nZWalks2DBContext.AddAsync(walk);
            await nZWalks2DBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk = await nZWalks2DBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walk == null) 
            {
                return null;
            }
            // Delete region
            nZWalks2DBContext.Walks.Remove(walk);
            await nZWalks2DBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetallAsync()
        {
            return await nZWalks2DBContext.Walks
                .Include(x=> x.Region)
                .Include(x=> x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            return await nZWalks2DBContext.Walks
                .Include(x => x.Region)
                .Include(x=> x.WalkDifficulty)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await nZWalks2DBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null) 
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Length = walk.Length;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.WalkDifficulty = walk.WalkDifficulty;

            await nZWalks2DBContext.SaveChangesAsync();
            return existingWalk;

        }
    }
}
