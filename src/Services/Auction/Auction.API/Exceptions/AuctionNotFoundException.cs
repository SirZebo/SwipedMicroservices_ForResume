namespace Auction.API.Exceptions;

using BuildingBlocks.Exceptions;
public class AuctionNotFoundException : NotFoundException
{
    public AuctionNotFoundException(Guid Id) : base("Product", Id)
    {
    }
}
