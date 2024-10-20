using BuildingBlocks.Exceptions;

namespace Bidding.Application.Exceptions;
public class AuctionNotFoundException : NotFoundException
{
    public AuctionNotFoundException(Guid id) : base("Auction", id)
    {
    }
}
