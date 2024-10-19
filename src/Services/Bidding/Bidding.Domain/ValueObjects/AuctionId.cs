namespace Bidding.Domain.ValueObjects;
public record AuctionId
{
    public Guid Value { get; }

    private AuctionId(Guid value)
        => Value = value;

    public static AuctionId Of(Guid value)
    {
        // Domain Validation
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("AuctionId cannot be empty");
        }

        return new AuctionId(value);
    }
}
