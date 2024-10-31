using Microsoft.AspNetCore.Mvc;
using MLService.Models;
using MLService.ML;

namespace MLService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MLController : ControllerBase
    {
        private readonly ProductCategorizationModel _model;

        public MLController(ProductCategorizationModel model)
        {
            _model = model;
        }

        [HttpPost("predict")]
        public ActionResult<ProductPrediction> Predict([FromBody] ProductData input)
        {
            if (input == null)
            {
                return BadRequest("Invalid input data.");
            }

            var prediction = _model.Predict(input);

            // Process individual scores, if necessary
            if (prediction.Score != null && prediction.Score.Length > 0)
            {
                float firstScore = prediction.Score[0];
                float secondScore = prediction.Score.Length > 1 ? prediction.Score[1] : 0.0f;
                float thirdScore = prediction.Score.Length > 2 ? prediction.Score[2] : 0.0f;
                Console.WriteLine($"Scores: {firstScore}, {secondScore}, {thirdScore}");
            }

            return Ok(prediction); 
        }

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok("ML Service is running");
        }
    }
}
