
namespace Auction.API.Auctions.GetAuctionById;


// public record GetAuctionByIdRequest();

public record GetAuctionByIdResponse(Models.Auction Auction);

public class GetAuctionByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/auctions/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetAuctionByIdQuery(id));

            var response = result.Adapt<GetAuctionByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetAuctionById")
        .Produces<GetAuctionByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Auction By Id")
        .WithDescription("Get Auction By Id");

    }
}
