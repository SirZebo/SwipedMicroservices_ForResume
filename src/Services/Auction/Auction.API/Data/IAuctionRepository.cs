﻿namespace Auction.API.Data;

public interface IAuctionRepository
{
    Task<IEnumerable<Models.Auction>> GetAuctions(CancellationToken cancellationToken = default);
    Task<Models.Auction> GetAuctionById(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Models.Auction>> GetAuctionByCategory(string category, CancellationToken cancellationToken = default); 
    Task<Models.Auction> StoreAuction(Models.Auction auction, CancellationToken cancellationToken = default);
    //Task<bool> DeleteAuction(Guid id, CancellationToken cancellationToken = default);

    // Updates the Auction and return the old Auction
    Task<Models.Auction> UpdateAuction(Models.Auction auction, CancellationToken cancellationToken= default);
}
