namespace Auction.API.Models;

public class Auction
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<string> Category { get; set; } = new();
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public DateTime EndingDate { get; set; }
    public decimal StartingPrice { get; set; }
}
