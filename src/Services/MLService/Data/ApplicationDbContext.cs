using Microsoft.EntityFrameworkCore;
using MLService.Models;

namespace MLService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Prediction> Predictions { get; set; }
        public DbSet<AuctionModelData> AuctionModelData { get; set; }  // Added DbSet for AuctionModelData
    }
}
