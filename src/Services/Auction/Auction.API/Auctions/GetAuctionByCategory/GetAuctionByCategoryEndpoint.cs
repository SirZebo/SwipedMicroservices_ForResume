namespace Auction.API.Auctions.GetAuctionByCategory;

// public record GetAuctionByCategoryRequest();
public record GetAuctionByCategoryResponse(IEnumerable<Models.Auction> Auctions);

public class GetAuctionByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/auctions/category/{category}", async (string category, ISender sender) =>
        {
            var result = await sender.Send(new GetAuctionByCategoryQuery(category));

            var response = result.Adapt<GetAuctionByCategoryResponse>();

            return Results.Ok(response);
        })
        .WithName("GetAuctionByCategory")
        .Produces<GetAuctionByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Auction By Category")
        .WithDescription("Get Auction By Category");
    }
}
