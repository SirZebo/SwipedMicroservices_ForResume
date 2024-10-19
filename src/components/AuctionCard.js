import React from 'react';
import { Link } from 'react-router-dom';

const AuctionCard = ({ auction }) => {
    return (
        <div className="card mb-4 shadow-sm">
            <div className="card-body">
                <h5 className="card-title">{auction.title}</h5>
                <p className="card-text">{auction.description}</p>
                <p className="card-text"><strong>Current Bid:</strong> ${auction.currentBid}</p>
                <Link to={`/auction/${auction.id}`} className="btn btn-primary">View Auction</Link>
            </div>
        </div>
    );
};

export default AuctionCard;
