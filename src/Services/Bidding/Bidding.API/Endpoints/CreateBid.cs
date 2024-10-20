using Bidding.Application.Bids.Commands.CreateBid;

namespace Bidding.API.Endpoints;

public record CreateBidRequest(BidDto Bid);
public record CreateBidResponse(Guid Id);

public class CreateBid : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/bids", async (CreateBidRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateBidCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateBidResponse>();

            return Results.Created($"/bids/{response.Id}", response);
        })
        .WithName("CreateBid")
        .Produces<CreateBidResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Bid")
        .WithDescription("Create Bid");
    }
}