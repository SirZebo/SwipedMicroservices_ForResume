using Bidding.Application.Bids.Commands.CreateBid;

namespace Bidding.API.Endpoints;

public record UpdateBidRequest(BidDto Bid);
public record UpdateBidResponse(bool IsSuccess);

public class UpdateBid : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/bids", async (UpdateBidRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateBidCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateBidResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateBid")
        .Produces<UpdateBidResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Bid")
        .WithDescription("Update Bid");
    }
}