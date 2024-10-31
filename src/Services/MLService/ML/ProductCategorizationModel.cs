using Microsoft.ML;
using MLService.Models;
using System.IO;

namespace MLService.ML
{
    public class ProductCategorizationModel
    {
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<ProductData, ProductPrediction> _predictionEngine;

        public ProductCategorizationModel()
        {
            _mlContext = new MLContext();
            var modelPath = Path.Combine(Environment.CurrentDirectory, "ML", "Models", "product_categorization_model.zip");

            // Load the model
            ITransformer loadedModel = _mlContext.Model.Load(modelPath, out _);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<ProductData, ProductPrediction>(loadedModel);
        }

        public ProductPrediction Predict(ProductData input)
        {
            return _predictionEngine.Predict(input);
        }
    }
}
