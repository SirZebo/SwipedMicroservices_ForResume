using BuildingBlocks.Exceptions;

namespace BiddingResult.API.Exceptions;

public class ContractNotFoundException : NotFoundException
{
    public ContractNotFoundException(Guid Id) : base("Contract", Id)
    {
    }
}
