namespace BiddingResult.API.BiddingResults.GetContractById;

// public record GetContractByIdRequest();

public record GetContractByIdResponse(Contract Contract);

public class GetContractByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/contracts/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetContractByIdQuery(id));

            var response = result.Adapt<GetContractByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetContractById")
        .Produces<GetContractByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Contract By Id")
        .WithDescription("Get Contract By Id");

    }
}
