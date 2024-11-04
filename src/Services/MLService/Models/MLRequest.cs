namespace MLService.Models
{
    public record MLResult
    {
        public Guid RequestId { get; set; }
        public List<string> Categories { get; set; } = new();
        public string Status { get; set; } = "Processed";
    }
}
