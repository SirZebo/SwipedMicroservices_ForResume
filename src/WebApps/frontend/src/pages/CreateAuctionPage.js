import React, { useState } from 'react';
import { createAuction } from '../services/auctionApi';

const CreateAuctionPage = () => {
    const [auction, setAuction] = useState({
        Name: '',
        Category: '',
        Description: '',
        ImageFile: '',
        EndingDate: '',
        StartingPrice: ''
    });

    const handleChange = (e) => {
        setAuction({ ...auction, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
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
    );
};

export default CreateAuctionPage;
