import axios from 'axios';

const biddingBaseUrl = process.env.REACT_APP_BIDDING_URL || 'https://localhost:6061';

export const getBiddingHealth = async () => {
    const response = await axios.get(`${biddingBaseUrl}/health`);
    return response.data;
};

export const updateBid = async (bidData) => {
    const response = await axios.put(`${biddingBaseUrl}/bids`, bidData);
    return response.data;
};
