using MediatR;


namespace BuildingBlocks.CQRS;

// Return a bool for ICommand with no response
public interface ICommand : ICommand<Unit>
{

}
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}

