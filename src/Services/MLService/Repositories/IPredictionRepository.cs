using System.Collections.Generic;
using System.Threading.Tasks;
using MLService.Models;

namespace MLService.Repositories
{
    public interface IPredictionRepository
    {
        Task SavePredictionAsync(Prediction prediction);
        Task<IEnumerable<Prediction>> GetAllPredictionsAsync();
        Task<Prediction> GetPredictionByIdAsync(int id);
    }
}
