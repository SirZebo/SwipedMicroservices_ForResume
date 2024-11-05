import axios from 'axios';

const biddingResultBaseUrl = process.env.REACT_APP_BIDDING_RESULT_URL || 'https://localhost:6062';

export const getAllContracts = async () => {
    const response = await axios.get(`${biddingResultBaseUrl}/contracts`);
    return response.data;
};

export const getContractById = async (id) => {
    const response = await axios.get(`${biddingResultBaseUrl}/contracts/${id}`);
    return response.data;
};
