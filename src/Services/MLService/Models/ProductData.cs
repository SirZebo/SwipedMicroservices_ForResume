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
         public string? Category { get; set; } // Make nullable if it's not always provided
    }
}
