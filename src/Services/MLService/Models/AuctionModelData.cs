using System.ComponentModel.DataAnnotations;

namespace MLService.Models
{
    public class AuctionModelData
    {
        [Key]
        public string AuctionId { get; set; }
        
        public string ModelData { get; set; }  // Modify the type based on what you need to store for the model data
    }
}
