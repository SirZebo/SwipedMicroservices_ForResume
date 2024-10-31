namespace MLService.Models
{
    public class Prediction
    {
        public int Id { get; set; }  // Primary key
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string PredictedCategory { get; set; }
        public float Probability { get; set; }
        public DateTime PredictionDate { get; set; }  // Timestamp to record when the prediction was made
    }
}
