namespace Bidding.Domain.Models;
public class Auction : Entity<AuctionId>
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    public decimal StartingPrice { get; set; }
    public DateTime EndingDate { get; set; }

    

    public static Auction Create(AuctionId id, string name, decimal startingPrice, DateTime endingDate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(startingPrice);

        var product = new Auction
        {
            Id = id,
            Name = name,
            Price = startingPrice,
            EndingDate = endingDate

        };

        return product;
    }

}

