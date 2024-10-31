namespace MLService.Models
{
    public class ProductPrediction
    {
        public string PredictedCategory { get; set; }
        public float[] Score { get; set; }  // Adjusted to handle multiple scores if needed
        public float Probability { get; set; }
    }
}
