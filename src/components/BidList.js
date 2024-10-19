import React from 'react';

const BidList = ({ bids }) => {
    return (
        <div className="mt-4">
            <h3>Bid History</h3>
            <ul className="list-group">
                {bids.map((bid, index) => (
                    <li key={index} className="list-group-item">
                        <strong>${bid.newBid}</strong> by {bid.bidder} at {new Date(bid.timestamp).toLocaleTimeString()}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default BidList;
