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
            return Ok(prediction);
        }
    }
}
