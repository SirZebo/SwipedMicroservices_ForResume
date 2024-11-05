<<<<<<< Updated upstream
import React, { useState, useEffect, useCallback } from 'react';
import { useParams } from 'react-router-dom';
import { getAuctionById, updateAuction } from '../services/auctionApi';

const EditAuctionPage = () => {
    const { id } = useParams();
    const [auction, setAuction] = useState({
        Name: '',
        Description: '',
        // other fields
    });

    const fetchAuction = useCallback(async () => {
        try {
            const data = await getAuctionById(id);
            setAuction(data);
        } catch (error) {
            console.error('Error fetching auction:', error);
            alert('Failed to fetch auction');
        }
    }, [id]);

    useEffect(() => {
        fetchAuction();
    }, [fetchAuction]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setAuction((prevAuction) => ({
            ...prevAuction,
            [name]: value,
        }));
=======
// src/pages/EditAuctionPage.js

import React, { useState, useEffect } from 'react';
import { getAuctionById, updateAuction } from '../services/auctionApi'; // Import updateAuction and getAuctionById
import { useParams } from 'react-router-dom';
import { Form, Button, Container } from 'react-bootstrap';

const EditAuctionPage = () => {
    const { id } = useParams(); // Extract auction ID from the URL
    const [auctionData, setAuctionData] = useState({
        name: '',
        category: '',
        description: '',
        startingPrice: '',
        endingDate: '',
        imageFile: '',
    });

    useEffect(() => {
        const fetchAuction = async () => {
            try {
                const auction = await getAuctionById(id);
                setAuctionData(auction);
            } catch (error) {
                console.error('Error fetching auction:', error);
            }
        };

        fetchAuction();
    }, [id]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setAuctionData((prevData) => ({ ...prevData, [name]: value }));
>>>>>>> Stashed changes
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
<<<<<<< Updated upstream
            await updateAuction(id, auction);
            alert('Auction updated successfully');
        } catch (error) {
            console.error('Error updating auction:', error);
            alert('Failed to update auction');
=======
            await updateAuction(id, auctionData);
            alert('Auction updated successfully!');
        } catch (error) {
            console.error('Error updating auction:', error);
>>>>>>> Stashed changes
        }
    };

    return (
<<<<<<< Updated upstream
        <form onSubmit={handleSubmit}>
            <h2>Edit Auction</h2>
            <input
                name="Name"
                value={auction.Name}
                onChange={handleChange}
                placeholder="Auction Name"
            />
            <input
                name="Description"
                value={auction.Description}
                onChange={handleChange}
                placeholder="Auction Description"
            />
            {/* Other fields */}
            <button type="submit">Save Changes</button>
        </form>
=======
        <Container>
            <h2>Edit Auction</h2>
            <Form onSubmit={handleSubmit}>
                {/* Form Fields */}
                <Button type="submit">Update Auction</Button>
            </Form>
        </Container>
>>>>>>> Stashed changes
    );
};

export default EditAuctionPage;
