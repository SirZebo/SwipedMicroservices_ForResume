import React, { useState, useEffect, useCallback } from 'react';
import { useParams } from 'react-router-dom';
<<<<<<< Updated upstream
import { getAuctionById } from '../services/auctionApi';
=======
import { getAuctionById, getBidsByAuctionId } from '../services/auctionApi';
import { Container, Row, Col, Card, ListGroup, Alert, Badge } from 'react-bootstrap';
>>>>>>> Stashed changes

const AuctionById = () => {
    const { id } = useParams();
    const [auction, setAuction] = useState(null);
<<<<<<< Updated upstream

    const fetchAuction = useCallback(async () => {
        const data = await getAuctionById(id);
        setAuction(data);
    }, [id]);

    useEffect(() => {
        fetchAuction();
    }, [fetchAuction]);
=======
    const [bids, setBids] = useState([]);

    const fetchAuctionDetails = useCallback(async () => {
        try {
            const auctionData = await getAuctionById(id);
            setAuction(auctionData);
        } catch (error) {
            console.error('Failed to fetch auction details:', error);
        }
    }, [id]);

    const fetchBids = useCallback(async () => {
        try {
            const bidsData = await getBidsByAuctionId(id);
            setBids(bidsData);
        } catch (error) {
            console.error('Failed to fetch bids:', error);
        }
    }, [id]);

    useEffect(() => {
        fetchAuctionDetails();
        fetchBids();
    }, [fetchAuctionDetails, fetchBids]);
>>>>>>> Stashed changes

    if (!auction) return <p>Loading...</p>;

    return (
<<<<<<< Updated upstream
        <div>
            <h2>Auction Details</h2>
            <p><strong>Name:</strong> {auction.Name}</p>
            <p><strong>Description:</strong> {auction.Description}</p>
        </div>
=======
        <Container className="mt-5">
            <Row className="justify-content-center">
                <Col md={10}>
                    <Card className="shadow-lg border-0">
                        <Row noGutters>
                            <Col md={5}>
                                <Card.Img
                                    variant="top"
                                    src={auction.ImageFile}
                                    alt={auction.Name}
                                    style={{ width: '100%', height: '100%', objectFit: 'cover', borderRadius: '0.5rem' }}
                                />
                            </Col>
                            <Col md={7}>
                                <Card.Body className="p-4">
                                    <Card.Title as="h2" className="mb-4">{auction.Name}</Card.Title>
                                    <Card.Text><strong>Description:</strong> {auction.Description}</Card.Text>
                                    <Card.Text><strong>Starting Price:</strong> <Badge bg="success">${auction.StartingPrice}</Badge></Card.Text>
                                    <Card.Text><strong>Ending Date:</strong> {new Date(auction.EndingDate).toLocaleString()}</Card.Text>
                                </Card.Body>
                            </Col>
                        </Row>
                    </Card>
                    
                    <h3 className="mt-5 mb-3">Bids</h3>
                    {bids.length > 0 ? (
                        <ListGroup variant="flush" className="shadow-sm">
                            {bids.map((bid, index) => (
                                <ListGroup.Item
                                    key={index}
                                    className="d-flex justify-content-between align-items-center p-3"
                                    style={{
                                        fontSize: '1.2rem',
                                        backgroundColor: '#f8f9fa',
                                        borderRadius: '0.3rem',
                                        marginBottom: '0.5rem',
                                        boxShadow: '0 2px 4px rgba(0, 0, 0, 0.1)'
                                    }}
                                >
                                    <span><strong>Customer:</strong> {bid.CustomerId}</span>
                                    <span><strong>Price:</strong> <Badge bg="primary" style={{ fontSize: '1.1rem' }}>${bid.Price}</Badge></span>
                                </ListGroup.Item>
                            ))}
                        </ListGroup>
                    ) : (
                        <Alert variant="info" className="mt-3">
                            No bids yet.
                        </Alert>
                    )}
                </Col>
            </Row>
        </Container>
>>>>>>> Stashed changes
    );
};

export default AuctionById;
