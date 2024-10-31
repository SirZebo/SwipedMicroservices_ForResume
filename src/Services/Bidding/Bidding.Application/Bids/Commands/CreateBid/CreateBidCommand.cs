using FluentValidation;

namespace Bidding.Application.Bids.Commands.CreateBid;
public record CreateBidCommand(BidDto Bid)
    : ICommand<CreateBidResult>;

public record CreateBidResult(Guid Id);

public class CreateBidCommandValidator : AbstractValidator<CreateBidCommand>
{
    public CreateBidCommandValidator()
    {
        RuleFor(x => x.Bid.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Bid.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.Bid.AuctionId).NotNull().WithMessage("AuctionId is required");
    }
}