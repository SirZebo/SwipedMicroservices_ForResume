namespace Auction.API.Auctions.CreateAuction;

public record CreateAuctionCommand(string Name, List<string> Category, string Description, string ImageFile, DateTime EndingDate, decimal Price)
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

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.EndingDate)
            .GreaterThan(DateTime.UtcNow.AddDays(1)).WithMessage("EndingDate must be at least 1 day later")
            .LessThan(DateTime.UtcNow.AddMonths(3)).WithMessage("EndingDate must be less than 3 months");
    }

}
internal class CreateAuctionCommandHandler
    (IDocumentSession session)
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
            Price = command.Price
        };

        // save to database
        session.Store(auction);
        await session.SaveChangesAsync(cancellationToken);

        // return result
        return new CreateAuctionResult(auction.Id);
    }
}
