using Microsoft.ML;
using MLService.Models;
using System;
using System.IO;

namespace MLService.ML
{
    public class ProductCategorizationTrainer
    {
        private readonly MLContext _mlContext;
        private readonly string _modelPath;

        public ProductCategorizationTrainer()
        {
            _mlContext = new MLContext();

            // Set solution root correctly by stepping up from MLTrainer to SwipedMicroservices
            var solutionRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."));

            // Set the model path to MLService/ML/Models directory under the solution root
            _modelPath = Path.Combine(solutionRoot, "src", "Services", "MLService", "ML", "Models", "product_categorization_model.zip");

            // Ensure the model directory exists
            var modelDirectory = Path.GetDirectoryName(_modelPath);
            if (!Directory.Exists(modelDirectory))
            {
                Directory.CreateDirectory(modelDirectory);
            }
        }

        public void TrainModel()
        {
            // Construct the data path relative to solution root
            var solutionRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."));
            var dataPath = Path.Combine(solutionRoot, "src", "Services", "MLService", "ML", "Data", "product_data.csv");

            if (!File.Exists(dataPath))
            {
                throw new FileNotFoundException($"Training data not found at path: {dataPath}");
            }

            // Load the training data from the specified CSV file path
            IDataView dataView = _mlContext.Data.LoadFromTextFile<ProductData>(dataPath, separatorChar: ',', hasHeader: true);

            // Define data processing pipeline
            var pipeline = _mlContext.Transforms.Text.FeaturizeText("ProductNameFeaturized", "ProductName")
                .Append(_mlContext.Transforms.Text.FeaturizeText("ProductDescriptionFeaturized", "ProductDescription"))
                .Append(_mlContext.Transforms.Concatenate("Features", "ProductNameFeaturized", "ProductDescriptionFeaturized"))
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label", "Category"))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedCategory", "PredictedLabel"));

            // Train the model
            var model = pipeline.Fit(dataView);

            // Save the model to a .zip file
            _mlContext.Model.Save(model, dataView.Schema, _modelPath);
            Console.WriteLine($"Model trained and saved at {_modelPath}");
        }
    }
}
