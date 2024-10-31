using MLService.Data;
using MLService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MLService.Repositories
{
    public class PredictionRepository : IPredictionRepository
    {
        private readonly ApplicationDbContext _context;

        public PredictionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SavePredictionAsync(Prediction prediction)
        {
            _context.Predictions.Add(prediction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Prediction>> GetAllPredictionsAsync()
        {
            return await _context.Predictions.ToListAsync();
        }

        public async Task<Prediction> GetPredictionByIdAsync(int id)
        {
            return await _context.Predictions.FindAsync(id);
        }
    }
}
