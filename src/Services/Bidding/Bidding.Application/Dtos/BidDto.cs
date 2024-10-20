namespace Bidding.Application.Dtos;
public record BidDto(
    Guid Id,
    Guid CustomerId,
    Guid AuctionId,
    decimal Price);