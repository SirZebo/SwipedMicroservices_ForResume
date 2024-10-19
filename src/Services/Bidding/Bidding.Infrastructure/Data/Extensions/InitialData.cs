namespace Bidding.Infrastructure.Data.Extensions;
internal class InitialData
{
    public static IEnumerable<Customer> Customers =>
        new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")), "mehmet", "mehmet@gmail.com"),
        Customer.Create(CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")), "john", "john@gmail.com")
    };
    public static IEnumerable<Auction> Products =>
        new List<Auction>
        {
            Auction.Create(AuctionId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), "IPhone X", 500, DateTime.UtcNow.AddDays(5)),
            Auction.Create(AuctionId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), "Samsung 10", 400, DateTime.UtcNow.AddDays(6)),
            Auction.Create(AuctionId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), "Huawei Plus", 650, DateTime.UtcNow.AddDays(7)),
            Auction.Create(AuctionId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), "Xiaomi Mi", 450, DateTime.UtcNow.AddDays(8))
        };

    public static IEnumerable<Bid> Bids
    {
        get
        {
            var bid1 = Bid.Create(
                            BidId.Of(Guid.NewGuid()),
                            AuctionId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")),
                            CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
                            550);

            var bid2 = Bid.Create(
                            BidId.Of(Guid.NewGuid()),
                            AuctionId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")),
                            CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")),
                            420);

            var bid3 = Bid.Create(
                            BidId.Of(Guid.NewGuid()),
                            AuctionId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")),
                            CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")),
                            660);

            var bid4 = Bid.Create(
                            BidId.Of(Guid.NewGuid()),
                            AuctionId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")),
                            CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
                            480);

            return new List<Bid> { bid1, bid2, bid3, bid4 };
        }
    }
}

