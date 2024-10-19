import React, { useState } from 'react';
import axios from 'axios';

const BidForm = ({ auctionId, currentBid }) => {
    const [bidAmount, setBidAmount] = useState('');

    const handleBidSubmit = (e) => {
        e.preventDefault();
        const bid = parseFloat(bidAmount);
        if (isNaN(bid) || bid <= currentBid) {
            alert('Please enter a valid bid higher than the current bid.');
            return;
        }

        // Send bid to the backend
        axios.post(`/api/auctions/${auctionId}/bids`, { bid })
            .then(response => {
                console.log('Bid placed successfully:', response.data);
                setBidAmount('');
            })
            .catch(error => {
                console.error('Error placing bid:', error);
                alert('Failed to place bid. Please try again.');
            });
    };

    return (
        <form onSubmit={handleBidSubmit} className="my-4">
            <div className="form-group">
                <label htmlFor="bidAmount">Your Bid</label>
                <input
                    type="number"
                    className="form-control"
                    id="bidAmount"
                    value={bidAmount}
                    onChange={(e) => setBidAmount(e.target.value)}
                    placeholder={`Enter a bid higher than $${currentBid}`}
                    required
                    min={currentBid + 1}
                />
            </div>
            <button type="submit" className="btn btn-success mt-2">Place Bid</button>
        </form>
    );
};

export default BidForm;
