namespace Auction.API.Auctions.CreateAuction;

public record CreateAuctionRequest(string Name, List<string> Category, string Description, string ImageFile, DateTime EndingDate, decimal Price);

public record CreateAuctionResponse(Guid Id);

public class CreateAuctionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auctions",
            async (CreateAuctionRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateAuctionCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateAuctionResponse>();

                return Results.Created($"/auctions/{response.Id}", response);
            })
            .WithName("CreateAuction")
            .Produces<CreateAuctionResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Auction")
            .WithDescription("Create Auction");

    }
}

