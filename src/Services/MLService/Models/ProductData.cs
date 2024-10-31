using Microsoft.ML.Data;

namespace MLService.Models
{
    public class ProductData
    {
        [LoadColumn(0)]
        public string ProductName { get; set; }

        [LoadColumn(1)]
        public string ProductDescription { get; set; }

        [LoadColumn(2)]
        public string Category { get; set; } // This is the label we want to predict
    }

    public class ProductPrediction
    {
        public string PredictedCategory { get; set; }
        public float Probability { get; set; }
        public float Score { get; set; }
    }
}
