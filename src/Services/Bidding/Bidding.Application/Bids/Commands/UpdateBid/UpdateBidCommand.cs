using FluentValidation;

namespace Bidding.Application.Bids.Commands.CreateBid;
public record UpdateBidCommand(BidDto Bid)
    : ICommand<UpdateBidResult>;

public record UpdateBidResult(bool IsSuccess);

public class UpdateBidCommandValidator : AbstractValidator<CreateBidCommand>
{
    public UpdateBidCommandValidator()
    {
        RuleFor(x => x.Bid.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Bid.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.Bid.AuctionId).NotNull().WithMessage("AuctionId is required");
    }
}