import React, { useState, useEffect } from 'react';
import { getAllContracts } from '../services/biddingResultApi';
import { Link } from 'react-router-dom';

const ContractsListPage = () => {
    const [contracts, setContracts] = useState([]);

    useEffect(() => {
        fetchContracts();
    }, []);

    const fetchContracts = async () => {
        try {
            const data = await getAllContracts();
            setContracts(data);
        } catch (error) {
            console.error('Error fetching contracts:', error);
            alert('Failed to fetch contracts');
        }
    };

    return (
        <div>
            <h2>Contracts List</h2>
            <ul>
                {contracts.map(contract => (
                    <li key={contract.Id}>
                        <h3>{contract.Name}</h3>
                        <Link to={`/contracts/${contract.Id}`}>View Details</Link>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default ContractsListPage;
