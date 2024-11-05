import React, { useState, useEffect, useCallback } from 'react';
import { useParams } from 'react-router-dom';
import { getAuctionById } from '../services/auctionApi';

const AuctionById = () => {
    const { id } = useParams();
    const [auction, setAuction] = useState(null);

    const fetchAuction = useCallback(async () => {
        const data = await getAuctionById(id);
        setAuction(data);
    }, [id]);

    useEffect(() => {
        fetchAuction();
    }, [fetchAuction]);

    if (!auction) return <p>Loading...</p>;

    return (
        <div>
            <h2>Auction Details</h2>
            <p><strong>Name:</strong> {auction.Name}</p>
            <p><strong>Description:</strong> {auction.Description}</p>
        </div>
    );
};

export default AuctionById;
