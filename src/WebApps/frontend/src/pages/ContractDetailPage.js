import React, { useState, useEffect, useCallback } from 'react';
import { useParams } from 'react-router-dom';
import { getContractById } from '../services/biddingResultApi';

const ContractDetailPage = () => {
    const { id } = useParams();
    const [contract, setContract] = useState(null);

    const fetchContract = useCallback(async () => {
        const data = await getContractById(id);
        setContract(data);
    }, [id]);

    useEffect(() => {
        fetchContract();
    }, [fetchContract]);

    if (!contract) return <p>Loading...</p>;

    return (
        <div>
            <h2>Contract Details</h2>
            <p><strong>Name:</strong> {contract.Name}</p>
            <p><strong>Description:</strong> {contract.Description}</p>
        </div>
    );
};

export default ContractDetailPage;
