<<<<<<< Updated upstream
import axios from 'axios';

const biddingBaseUrl = process.env.REACT_APP_BIDDING_URL || 'https://localhost:6061';

export const getBiddingHealth = async () => {
    const response = await axios.get(`${biddingBaseUrl}/health`);
    return response.data;
};

export const updateBid = async (bidData) => {
    const response = await axios.put(`${biddingBaseUrl}/bids`, bidData);
    return response.data;
=======
import fetch from 'node-fetch';

const biddingBaseUrl = 'http://localhost:6061';

export const updateBid = async (bidData) => {
    const response = await fetch(`${biddingBaseUrl}/bids`, {
        method: 'POST', // Assuming JSON Server interprets POST for adding/updating
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(bidData),
    });

    if (!response.ok) {
        throw new Error('Failed to update bid');
    }

    return response.json();
>>>>>>> Stashed changes
};
