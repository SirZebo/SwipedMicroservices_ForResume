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
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await updateAuction(id, auction);
            alert('Auction updated successfully');
        } catch (error) {
            console.error('Error updating auction:', error);
            alert('Failed to update auction');
        }
    };

    return (
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
    );
};

export default EditAuctionPage;
