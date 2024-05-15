using NZWalks2.API.Models.Domain;
using NZWalks2.API.Data;
using Microsoft.EntityFrameworkCore;

namespace NZWalks2.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalks2DBContext nZWalks2DBContext;

        public WalkDifficultyRepository(NZWalks2DBContext nZWalks2DBContext)
        {
            this.nZWalks2DBContext = nZWalks2DBContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalks2DBContext.WalkDifficulty.AddAsync(walkDifficulty);
            await nZWalks2DBContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var existingwalkDifficulty = await nZWalks2DBContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (existingwalkDifficulty == null) 
            {
                return null;
            }
            nZWalks2DBContext.WalkDifficulty.Remove(existingwalkDifficulty);
            nZWalks2DBContext.SaveChangesAsync();
            return existingwalkDifficulty;

        }

        public async Task<IEnumerable<WalkDifficulty>> GetallAsync()
        {
            return await nZWalks2DBContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsyncId(Guid id)
        {
            return await nZWalks2DBContext.WalkDifficulty.FindAsync(id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await nZWalks2DBContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalkDifficulty == null) 
            {
                return null;
            }

            existingWalkDifficulty.Code = walkDifficulty.Code;
            await nZWalks2DBContext.SaveChangesAsync();

            return existingWalkDifficulty;

        }
    }
}
