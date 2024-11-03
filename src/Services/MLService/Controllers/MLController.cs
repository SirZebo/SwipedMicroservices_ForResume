using Microsoft.AspNetCore.Mvc;
using MLService.Models;
using MLService.ML;
using MLService.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MLService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MLController : ControllerBase
    {
        private readonly ProductCategorizationModel _model;
        private readonly IPredictionRepository _predictionRepository;

        public MLController(ProductCategorizationModel model, IPredictionRepository predictionRepository)
        {
            _model = model;
            _predictionRepository = predictionRepository;
        }

        [HttpPost("predict")]
        public async Task<ActionResult<Prediction>> Predict([FromBody] ProductData input)
        {
            if (input == null)
            {
                return BadRequest("Invalid input data.");
            }

            var predictionResult = _model.Predict(input);

            var predictionRecord = new Prediction
            {
                ProductName = input.ProductName,
                ProductDescription = input.ProductDescription,
                PredictedCategory = predictionResult.PredictedCategory,
                Probability = predictionResult.Probability,
                PredictionDate = DateTime.UtcNow
            };

            await _predictionRepository.SavePredictionAsync(predictionRecord);

            return Ok(predictionRecord);
        }

        [HttpGet("predictions")]
        public async Task<ActionResult<IEnumerable<Prediction>>> GetAllPredictions()
        {
            var predictions = await _predictionRepository.GetAllPredictionsAsync();
            return Ok(predictions);
        }

        [HttpGet("predictions/{id}")]
        public async Task<ActionResult<Prediction>> GetPredictionById(int id)
        {
            var prediction = await _predictionRepository.GetPredictionByIdAsync(id);
            if (prediction == null)
            {
                return NotFound();
            }
            return Ok(prediction);
        }

        // Health Check endpoint added here
        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok("ML Service is running");
        }
    }
}
