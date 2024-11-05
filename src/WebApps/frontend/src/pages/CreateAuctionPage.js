<<<<<<< Updated upstream
import React, { useState } from 'react';
import { createAuction } from '../services/auctionApi';

const CreateAuctionPage = () => {
    const [auction, setAuction] = useState({
=======
import React, { useState, useEffect } from 'react';
import { createAuction, getAllAuctions } from '../services/auctionApi';
import { Form, Button, Container, Row, Col } from 'react-bootstrap';

const CreateAuctionPage = () => {
    const [auction, setAuction] = useState({
        id: '',
>>>>>>> Stashed changes
        Name: '',
        Category: '',
        Description: '',
        ImageFile: '',
        EndingDate: '',
        StartingPrice: ''
    });
<<<<<<< Updated upstream
=======
    const [nextId, setNextId] = useState(1);

    useEffect(() => {
        const fetchAuctions = async () => {
            try {
                const auctions = await getAllAuctions();
                if (auctions.length > 0) {
                    const highestId = Math.max(...auctions.map(a => parseInt(a.id)));
                    setNextId(highestId + 1);
                }
            } catch (error) {
                console.error('Failed to fetch auctions:', error);
            }
        };
        fetchAuctions();
    }, []);
>>>>>>> Stashed changes

    const handleChange = (e) => {
        setAuction({ ...auction, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
<<<<<<< Updated upstream
        await createAuction(auction);
        window.location.href = '/'; // Redirect to list after creation
    };

    return (
        <form onSubmit={handleSubmit}>
            <input name="Name" placeholder="Name" onChange={handleChange} required />
            <input name="Category" placeholder="Category" onChange={handleChange} required />
            <input name="Description" placeholder="Description" onChange={handleChange} required />
            <input name="ImageFile" placeholder="Image File URL" onChange={handleChange} required />
            <input name="EndingDate" placeholder="Ending Date" onChange={handleChange} required />
            <input name="StartingPrice" placeholder="Starting Price" onChange={handleChange} required />
            <button type="submit">Create Auction</button>
        </form>
=======
        await createAuction({ ...auction, id: nextId.toString() });
        window.location.href = '/';
    };

    return (
        <Container className="mt-5" style={{ backgroundColor: '#f8f9fa', borderRadius: '15px', padding: '20px' }}>
            <Row className="justify-content-center">
                <Col md={6}>
                    <h2 className="mb-4">Create Auction</h2>
                    <Form onSubmit={handleSubmit}>
                        <Form.Group controlId="formName" className="mb-3">
                            <Form.Label>Name</Form.Label>
                            <Form.Control 
                                type="text"
                                name="Name"
                                placeholder="Enter auction name"
                                onChange={handleChange}
                                required 
                            />
                        </Form.Group>

                        <Form.Group controlId="formCategory" className="mb-3">
                            <Form.Label>Category</Form.Label>
                            <Form.Control 
                                type="text"
                                name="Category"
                                placeholder="Enter category"
                                onChange={handleChange}
                                required 
                            />
                        </Form.Group>

                        <Form.Group controlId="formDescription" className="mb-3">
                            <Form.Label>Description</Form.Label>
                            <Form.Control 
                                as="textarea"
                                name="Description"
                                placeholder="Enter description"
                                rows={3}
                                onChange={handleChange}
                                required 
                            />
                        </Form.Group>

                        <Form.Group controlId="formImageFile" className="mb-3">
                            <Form.Label>Image File URL</Form.Label>
                            <Form.Control 
                                type="url"
                                name="ImageFile"
                                placeholder="Enter image URL"
                                onChange={handleChange}
                                required 
                            />
                        </Form.Group>

                        <Form.Group controlId="formEndingDate" className="mb-3">
                            <Form.Label>Ending Date</Form.Label>
                            <Form.Control 
                                type="datetime-local"
                                name="EndingDate"
                                onChange={handleChange}
                                required 
                            />
                        </Form.Group>

                        <Form.Group controlId="formStartingPrice" className="mb-3">
                            <Form.Label>Starting Price</Form.Label>
                            <Form.Control 
                                type="number"
                                name="StartingPrice"
                                placeholder="Enter starting price"
                                onChange={handleChange}
                                required 
                                min="0"
                            />
                        </Form.Group>

                        <Button
                            variant="primary"
                            type="submit"
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
                            Create Auction
                        </Button>
                    </Form>
                </Col>
            </Row>
        </Container>
>>>>>>> Stashed changes
    );
};

export default CreateAuctionPage;
