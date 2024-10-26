namespace BuildingBlocks.Messaging.Events;
public class AuctionCreatedEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime EndingDate { get; set; }
    public decimal StartingPrice { get; set; }
}
