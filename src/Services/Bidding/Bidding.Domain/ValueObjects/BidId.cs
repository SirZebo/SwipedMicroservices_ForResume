namespace Bidding.Domain.ValueObjects;
public record BidId
{
    public Guid Value { get; }

    private BidId(Guid value)
        => Value = value;

    public static BidId Of(Guid value)
    {
        // Domain Validation
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("BidId cannot be empty");
        }

        return new BidId(value);
    }
}
