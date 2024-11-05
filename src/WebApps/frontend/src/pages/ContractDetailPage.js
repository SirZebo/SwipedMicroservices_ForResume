<<<<<<< Updated upstream
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
=======
import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { getContractById } from '../services/biddingResultApi';
import { Container, Card } from 'react-bootstrap';

const ContractDetailPage = () => {
    const { id } = useParams(); // Get the ID from the URL
    const [contract, setContract] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchContract = async () => {
            try {
                const data = await getContractById(id); // Fetch contract by ID
                setContract(data);
            } catch (error) {
                console.error('Error fetching contract details:', error);
                setError('Failed to fetch contract details');
            } finally {
                setLoading(false);
            }
        };
        fetchContract();
    }, [id]);

    if (loading) return <p>Loading...</p>;
    if (error) return <p>{error}</p>;

    return (
        <Container className="my-4">
            {contract ? (
                <Card className="shadow-sm">
                    <Card.Body>
                        <Card.Title>{contract.Name}</Card.Title>
                        <Card.Text><strong>Description:</strong> {contract.Description}</Card.Text>
                        {/* Add other contract details if needed */}
                    </Card.Body>
                </Card>
            ) : (
                <p>No contract found with this ID.</p>
            )}
        </Container>
>>>>>>> Stashed changes
    );
};

export default ContractDetailPage;
