using BuildingBlocks.Exceptions;

namespace Bidding.Application.Exceptions;
public class InvalidBidPriceException : BadRequestException
{
    public InvalidBidPriceException(Guid auctionId, Guid customerId, decimal price) : base($"Bid Price {price} is invalid from Customer ${customerId} on Auction {auctionId}")
    {
    }
}
