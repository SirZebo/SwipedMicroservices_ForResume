using Microsoft.AspNetCore.Mvc;
using MLService.Models;
using MLService.ML;
using MLService.Data;
using MLService.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;  // Make sure to import this for DbContext

namespace MLService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MLController : ControllerBase
    {
        private readonly ProductCategorizationModel _model;
        private readonly IPredictionRepository _predictionRepository;
        private readonly ApplicationDbContext _context;  // Adding ApplicationDbContext to interact with database

        public MLController(ProductCategorizationModel model, IPredictionRepository predictionRepository, ApplicationDbContext context)
        {
            _model = model;
            _predictionRepository = predictionRepository;
            _context = context;
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

        // Health Check endpoint
        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok("ML Service is running");
        }

<<<<<<< Updated upstream
        // RabbitMQ Listener endpoint added here
        [HttpGet("start-rabbitmq-listener")]
        public IActionResult StartRabbitMQListener()
        {
            var rabbitMQService = new RabbitMQService();
            rabbitMQService.ConnectAndListen();
            return Ok("RabbitMQ Listener started.");
=======
        // New endpoint to store auction data
        [HttpPost("store-auction")]
        public async Task<IActionResult> StoreAuctionData([FromBody] AuctionModelData auctionData)
        {
            if (auctionData == null || string.IsNullOrEmpty(auctionData.AuctionId))
            {
                return BadRequest("AuctionId and model data are required.");
            }

            _context.AuctionModelData.Add(auctionData);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Auction model data stored successfully" });
        }

        // New endpoint to retrieve auction data by auctionId
        [HttpGet("get-auction/{auctionId}")]
        public async Task<IActionResult> GetAuctionData(string auctionId)
        {
            var auctionData = await _context.AuctionModelData
                .FirstOrDefaultAsync(a => a.AuctionId == auctionId);

            if (auctionData == null)
            {
                return NotFound("Auction model data not found.");
            }

            return Ok(auctionData);
>>>>>>> Stashed changes
        }
    }
}
