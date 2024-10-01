namespace Auction.API.Exceptions;

using BuildingBlocks.Exceptions;
public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid Id) : base("Product", Id)
    {
    }
}
