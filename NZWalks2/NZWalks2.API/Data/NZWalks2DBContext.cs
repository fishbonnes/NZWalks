using Microsoft.EntityFrameworkCore;
using NZWalks2.API.Models.Domain;

namespace NZWalks2.API.Data
{
    public class NZWalks2DBContext: DbContext
    {
        public NZWalks2DBContext(DbContextOptions<NZWalks2DBContext> options): base(options) 
        {
            
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
    }
}
