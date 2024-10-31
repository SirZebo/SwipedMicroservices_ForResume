using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Auction.API.Auctions.CreateAuction;

public record CreateAuctionCommand(string Name, List<string> Category, string Description, string ImageFile, DateTime EndingDate, decimal StartingPrice)
    : ICommand<CreateAuctionResult>;

public record CreateAuctionResult(Guid Id);

public class CreateAuctionCommandValidator : AbstractValidator<CreateAuctionCommand>
{
    public CreateAuctionCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required");

        RuleFor(x => x.ImageFile)
            .NotEmpty().WithMessage("ImageFile is required");

        RuleFor(x => x.StartingPrice)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.EndingDate)
            .GreaterThan(DateTime.UtcNow.AddSeconds(1)).WithMessage("EndingDate must be at least 1 day later")
            .LessThan(DateTime.UtcNow.AddMonths(3)).WithMessage("EndingDate must be less than 3 months");
    }

}
internal class CreateAuctionCommandHandler
    (IAuctionRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CreateAuctionCommand, CreateAuctionResult>
{
    // Business logic to create a product
    public async Task<CreateAuctionResult> Handle(CreateAuctionCommand command, CancellationToken cancellationToken)
    {
        var auction = new Models.Auction
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            EndingDate = command.EndingDate,
            StartingPrice = command.StartingPrice
        };

        // save to database
        await repository.StoreAuction(auction);

        var eventMessage = auction.Adapt<AuctionCreatedEvent>();
        eventMessage.AuctionId = auction.Id;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        // return result
        return new CreateAuctionResult(auction.Id);
    }
}
