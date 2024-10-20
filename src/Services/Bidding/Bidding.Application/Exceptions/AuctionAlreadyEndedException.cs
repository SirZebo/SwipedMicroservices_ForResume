using BuildingBlocks.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bidding.Application.Exceptions;
public class AuctionAlreadyEndedException : BadRequestException
{
    public AuctionAlreadyEndedException(Guid id) : base($"Auction id: {id} has already ended.", id.ToString())
    {
    }
}
