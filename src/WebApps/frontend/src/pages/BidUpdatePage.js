import React, { useState } from 'react';
import { updateBid } from '../services/biddingApi';

const BidUpdatePage = () => {
    const [bid, setBid] = useState({
        CustomerId: '',
        AuctionId: '',
        Price: ''
    });

    const handleChange = (e) => {
        setBid({ ...bid, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await updateBid({ Bid: bid });
            alert('Bid updated successfully');
        } catch (error) {
            console.error('Error updating bid:', error);
            alert('Failed to update bid');
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Update Bid</h2>
            <input
                name="CustomerId"
                placeholder="Customer ID"
                value={bid.CustomerId}
                onChange={handleChange}
                required
            />
            <input
                name="AuctionId"
                placeholder="Auction ID"
                value={bid.AuctionId}
                onChange={handleChange}
                required
            />
            <input
                name="Price"
                placeholder="Price"
                value={bid.Price}
                onChange={handleChange}
                required
                type="number"
                min="0"
            />
            <button type="submit">Update Bid</button>
        </form>
    );
};

export default BidUpdatePage;
