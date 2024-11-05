import React, { useState } from 'react';
import { updateBid } from '../services/biddingApi';
<<<<<<< Updated upstream
=======
import { useNavigate } from 'react-router-dom';
import { Form, Button, Alert, Container } from 'react-bootstrap';
>>>>>>> Stashed changes

const BidUpdatePage = () => {
    const [bid, setBid] = useState({
        CustomerId: '',
        AuctionId: '',
        Price: ''
    });
<<<<<<< Updated upstream
=======
    const [message, setMessage] = useState(null);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
>>>>>>> Stashed changes

    const handleChange = (e) => {
        setBid({ ...bid, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
<<<<<<< Updated upstream
        try {
            await updateBid({ Bid: bid });
            alert('Bid updated successfully');
        } catch (error) {
            console.error('Error updating bid:', error);
            alert('Failed to update bid');
=======
        setMessage(null);
        setError(null);
        try {
            await updateBid(bid);
            setMessage('Bid updated successfully!');
            setTimeout(() => navigate(`/auctions/${bid.AuctionId}`), 1000);
        } catch (error) {
            console.error('Error updating bid:', error);
            setError('Failed to update bid. Please check the details and try again.');
>>>>>>> Stashed changes
        }
    };

    return (
<<<<<<< Updated upstream
        <form onSubmit={handleSubmit}>
            <h2>Update Bid</h2>
            <input
                name="CustomerId"
                placeholder="Customer ID"
                value={bid.CustomerId}
                onChange={handleChange}
                required
            />
            <input
                name="AuctionId"
                placeholder="Auction ID"
                value={bid.AuctionId}
                onChange={handleChange}
                required
            />
            <input
                name="Price"
                placeholder="Price"
                value={bid.Price}
                onChange={handleChange}
                required
                type="number"
                min="0"
            />
            <button type="submit">Update Bid</button>
        </form>
=======
        <Container className="mt-5" style={{ backgroundColor: '#f8f9fa', borderRadius: '15px', padding: '20px' }}>
            <h2 className="text-center mb-4">Update Bid</h2>
            {message && <Alert variant="success">{message}</Alert>}
            {error && <Alert variant="danger">{error}</Alert>}
            <Form onSubmit={handleSubmit} className="p-3 shadow-sm rounded">
                <Form.Group controlId="customerId" className="mb-3">
                    <Form.Label>Customer Name</Form.Label>
                    <Form.Control
                        type="text"
                        name="CustomerId"
                        placeholder="Enter Customer ID"
                        value={bid.CustomerId}
                        onChange={handleChange}
                        required
                    />
                </Form.Group>
                <Form.Group controlId="auctionId" className="mb-3">
                    <Form.Label>Auction ID</Form.Label>
                    <Form.Control
                        type="text"
                        name="AuctionId"
                        placeholder="Enter Auction ID"
                        value={bid.AuctionId}
                        onChange={handleChange}
                        required
                    />
                </Form.Group>
                <Form.Group controlId="price" className="mb-4">
                    <Form.Label>Price</Form.Label>
                    <Form.Control
                        type="number"
                        name="Price"
                        placeholder="Enter Bid Price"
                        value={bid.Price}
                        onChange={handleChange}
                        required
                        min="0"
                    />
                </Form.Group>
                <Button
                    type="submit"
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
                    Update Bid
                </Button>
            </Form>
        </Container>
>>>>>>> Stashed changes
    );
};

export default BidUpdatePage;
