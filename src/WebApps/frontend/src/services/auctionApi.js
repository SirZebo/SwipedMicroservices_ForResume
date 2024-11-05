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
};
