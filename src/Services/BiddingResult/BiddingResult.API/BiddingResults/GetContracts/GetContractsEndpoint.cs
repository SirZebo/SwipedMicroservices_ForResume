namespace BiddingResult.API.BiddingResults.GetContracts;

public record GetContractsRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetContractsResponse(IEnumerable<Contract> Contracts);

public class GetContractsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/contracts", async ([AsParameters] GetContractsRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetContractQuery>();

            var result = await sender.Send(query);

            var response = result.Adapt<GetContractsResponse>();

            return Results.Ok(response);
        })
        .WithName("GetContracts")
        .Produces<GetContractsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Contracts")
        .WithDescription("Get Contracts");

    }
}