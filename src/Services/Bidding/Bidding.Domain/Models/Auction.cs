namespace Bidding.Domain.Models;
public class Auction : Entity<AuctionId>
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    public DateTime EndingDate { get; set; }

    public static Auction Create(AuctionId id, string name, decimal price, DateTime endingDate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var product = new Auction
        {
            Id = id,
            Name = name,
            Price = price,
            EndingDate = endingDate

        };

        return product;
    }

}

