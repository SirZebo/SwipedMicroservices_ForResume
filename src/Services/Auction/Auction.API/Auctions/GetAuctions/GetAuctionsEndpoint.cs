namespace Auction.API.Auctions.GetAuctions;

public record GetAuctionsRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetAuctionsResponse(IEnumerable<Models.Auction> Auctions);

public class GetAuctionsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/auctions", async (ISender sender) =>
        {
            var result = await sender.Send(new GetAuctionsQuery());

            var response = result.Adapt<GetAuctionsResponse>();

            return Results.Ok(response);
        })
        .WithName("GetAuctions")
        .Produces<GetAuctionsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Auctions")
        .WithDescription("Get Auctions");

    }
}
