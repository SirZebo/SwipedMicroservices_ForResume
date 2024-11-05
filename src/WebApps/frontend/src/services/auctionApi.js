<<<<<<< Updated upstream
import axios from 'axios';

const auctionUrl = 'https://localhost:6060/auctions';

export const getAllAuctions = async () => {
    const response = await axios.get(auctionUrl);
    return response.data;
};

export const getAuctionById = async (id) => {
    const response = await axios.get(`${auctionUrl}/${id}`);
    return response.data;
};

export const createAuction = async (auctionData) => {
    const response = await axios.post(auctionUrl, auctionData);
    return response.data;
};

export const updateAuction = async (id, auctionData) => {
    const response = await axios.put(`${auctionUrl}/${id}`, auctionData);
    return response.data;
};

// Add deleteAuction if your backend supports DELETE
export const deleteAuction = async (id) => {
    const response = await axios.delete(`${auctionUrl}/${id}`);
    return response.data;
=======
import fetch from 'node-fetch';

const auctionBaseUrl = 'http://localhost:6060';
const biddingBaseUrl = 'http://localhost:6061';

// Fetch all auctions
export const getAllAuctions = async () => {
    const response = await fetch(`${auctionBaseUrl}/auctions`);
    if (!response.ok) {
        throw new Error('Failed to fetch auctions');
    }
    return response.json();
};

// Fetch an auction by ID
export const getAuctionById = async (id) => {
    const response = await fetch(`${auctionBaseUrl}/auctions/${id}`);
    if (!response.ok) {
        throw new Error('Failed to fetch auction details');
    }
    return response.json();
};

// Create a new auction
export const createAuction = async (auctionData) => {
    const response = await fetch(`${auctionBaseUrl}/auctions`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(auctionData),
    });
    if (!response.ok) {
        throw new Error('Failed to create auction');
    }
    return response.json();
};

// Update an existing auction
export const updateAuction = async (id, auctionData) => {
    const response = await fetch(`${auctionBaseUrl}/auctions/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(auctionData),
    });
    if (!response.ok) {
        throw new Error('Failed to update auction');
    }
    return response.json();
};

// Fetch bids for a specific auction by ID
export const getBidsByAuctionId = async (auctionId) => {
    const response = await fetch(`${biddingBaseUrl}/bids?AuctionId=${auctionId}`);
    if (!response.ok) {
        throw new Error('Failed to fetch bids for auction');
    }
    return response.json();
>>>>>>> Stashed changes
};
