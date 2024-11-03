namespace Auction.API.Auctions.UpdateAuction;

public record UpdateAuctionCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, DateTime EndingDate, decimal StartingPrice) : ICommand<UpdateAuctionResult>;

public record UpdateAuctionResult(bool IsSuccess);

public class UpdateAuctionValidator : AbstractValidator<UpdateAuctionCommand>
{
    public UpdateAuctionValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Auction Id is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

        RuleFor(x => x.StartingPrice)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

public class UpdateAuctionCommandHandler
{
}
