<<<<<<< Updated upstream
import axios from 'axios';

const biddingResultBaseUrl = process.env.REACT_APP_BIDDING_RESULT_URL || 'https://localhost:6062';

export const getAllContracts = async () => {
    const response = await axios.get(`${biddingResultBaseUrl}/contracts`);
    return response.data;
};

export const getContractById = async (id) => {
    const response = await axios.get(`${biddingResultBaseUrl}/contracts/${id}`);
    return response.data;
=======
// src/services/biddingResultApi.js

const biddingResultBaseUrl = 'http://localhost:6062';

export const getAllContracts = async () => {
    try {
        const response = await fetch(`${biddingResultBaseUrl}/contracts`);
        if (!response.ok) {
            throw new Error('Failed to fetch contracts');
        }
        const data = await response.json();
        console.log('API response data for all contracts:', data); // Log the raw API response
        return data;
    } catch (error) {
        console.error('Error in getAllContracts:', error);
        throw error;
    }
};

export const getContractById = async (id) => {
    try {
        const response = await fetch(`${biddingResultBaseUrl}/contracts/${id}`);
        if (!response.ok) {
            throw new Error('Failed to fetch contract');
        }
        const data = await response.json();
        console.log('API response data for contract by ID:', data); // Log the contract details
        return data;
    } catch (error) {
        console.error('Error in getContractById:', error);
        throw error;
    }
>>>>>>> Stashed changes
};
