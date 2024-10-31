using Microsoft.EntityFrameworkCore;
using MLService.Models;  

namespace MLService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Prediction> Predictions { get; set; }  
    }
}
