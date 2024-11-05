<<<<<<< Updated upstream
import React, { useState, useEffect } from 'react';
import { getAllContracts } from '../services/biddingResultApi';
import { Link } from 'react-router-dom';

const ContractsListPage = () => {
    const [contracts, setContracts] = useState([]);
=======
// src/pages/ContractsListPage.js

import React, { useState, useEffect } from 'react';
import { getAllContracts } from '../services/biddingResultApi';
import { Link } from 'react-router-dom';
import { Container, Card, Button, Row, Col, Alert } from 'react-bootstrap';

const ContractsListPage = () => {
    const [contracts, setContracts] = useState([]);
    const [error, setError] = useState(null);
>>>>>>> Stashed changes

    useEffect(() => {
        fetchContracts();
    }, []);

    const fetchContracts = async () => {
        try {
            const data = await getAllContracts();
<<<<<<< Updated upstream
            setContracts(data);
        } catch (error) {
            console.error('Error fetching contracts:', error);
            alert('Failed to fetch contracts');
=======
            console.log('Fetched contracts data:', data); // Log to see the fetched data
            if (data && Array.isArray(data.contracts)) {
                setContracts(data.contracts);
            } else {
                setContracts([]); // Set to empty array if data is not an array
                setError('No contracts found');
            }
        } catch (error) {
            console.error('Error fetching contracts:', error);
            setError('Failed to fetch contracts');
>>>>>>> Stashed changes
        }
    };

    return (
<<<<<<< Updated upstream
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
=======
        <Container className="my-4">
            <h2>Contracts List</h2>
            {error && <Alert variant="danger">{error}</Alert>}
            <Row>
                {contracts.length > 0 ? (
                    contracts.map(contract => (
                        <Col md={4} key={contract.id}>
                            <Card className="mb-4 shadow-sm">
                                <Card.Body>
                                    <Card.Title>{contract.Name}</Card.Title>
                                    <Card.Text><strong>Description:</strong> {contract.Description}</Card.Text>
                                    <Link to={`/contracts/${contract.id}`}>
                                        <Button variant="primary">View Details</Button>
                                    </Link>
                                </Card.Body>
                            </Card>
                        </Col>
                    ))
                ) : (
                    !error && <p>No contracts available.</p>
                )}
            </Row>
        </Container>
>>>>>>> Stashed changes
    );
};

export default ContractsListPage;
