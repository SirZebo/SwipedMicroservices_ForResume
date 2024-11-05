import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
<<<<<<< Updated upstream
import { getAllAuctions, deleteAuction } from '../services/auctionApi';

const AuctionListPage = () => {
    const [auctions, setAuctions] = useState([]);

    useEffect(() => {
        fetchAuctions();
    }, []);

    const fetchAuctions = async () => {
        const data = await getAllAuctions();
        setAuctions(data);
    };

    const handleDelete = async (id) => {
        await deleteAuction(id);
        fetchAuctions(); // Refresh the list after deletion
    };

    return (
        <div>
            <h2>All Auctions</h2>
            <Link to="/create" className="btn btn-primary">Create New Auction</Link>
            <ul>
                {auctions.map(auction => (
                    <li key={auction.Id}>
                        <h3>{auction.Name}</h3>
                        <Link to={`/auctions/${auction.Id}`}>View</Link>
                        <Link to={`/auctions/edit/${auction.Id}`}>Edit</Link>
                        <button onClick={() => handleDelete(auction.Id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
=======
import { getAllAuctions } from '../services/auctionApi';
import { Card, Container, Row, Col, Button } from 'react-bootstrap';

const AuctionListPage = () => {
    const [auctions, setAuctions] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchAuctions = async () => {
            try {
                const data = await getAllAuctions();
                setAuctions(data);
            } catch (error) {
                console.error('Error fetching auctions:', error);
                setError('Failed to fetch auctions');
            }
        };
        fetchAuctions();
    }, []);

    if (error) return <p className="text-danger text-center mt-4">{error}</p>;

    return (
        <Container className="mt-5" style={{ maxWidth: '1200px', margin: '0 auto' }}>
            <h2 className="my-4 text-center">Ongoing Auctions</h2>
            <Row>
                {auctions.map((auction) => (
                    <Col key={auction.id} sm={12} md={6} lg={4} className="mb-4 d-flex align-items-stretch">
                        <Card className="h-100 shadow-lg border-0" style={{ borderRadius: '15px', width: '100%' }}>
                            <div className="d-flex justify-content-center mt-3">
                                <Card.Img
                                    variant="top"
                                    src={auction.ImageFile || 'https://via.placeholder.com/240'}
                                    alt={auction.Name}
                                    style={{ width: '240px', height: '240px', objectFit: 'cover', borderRadius: '10px' }}
                                />
                            </div>
                            <Card.Body className="text-center d-flex flex-column" style={{ backgroundColor: '#f8f9fa', borderRadius: '15px', flexGrow: 1 }}>
                                <Card.Title className="mt-2" style={{ lineHeight: '1.2', fontSize: '1.3rem' }}>{auction.Name}</Card.Title>
                                <Card.Text className="text-muted mb-4" style={{ fontSize: '1rem', lineHeight: '1.4' }}>{auction.Description}</Card.Text>
                                <div className="mt-auto">
                                    <Button
                                        as={Link}
                                        to={`/auctions/${auction.id}`}
                                        variant="primary"
                                        className="w-100"
                                        style={{ backgroundColor: '#343a40', borderColor: '#343a40', transition: 'background-color 0.3s ease, border-color 0.3s ease' }}
                                        onMouseEnter={(e) => {
                                            e.target.style.backgroundColor = '#0056b3';
                                            e.target.style.borderColor = '#0056b3';
                                        }}
                                        onMouseLeave={(e) => {
                                            e.target.style.backgroundColor = '#343a40';
                                            e.target.style.borderColor = '#343a40';
                                        }}
                                    >
                                        View Auction
                                    </Button>
                                </div>
                            </Card.Body>
                        </Card>
                    </Col>
                ))}
            </Row>
        </Container>
>>>>>>> Stashed changes
    );
};

export default AuctionListPage;
