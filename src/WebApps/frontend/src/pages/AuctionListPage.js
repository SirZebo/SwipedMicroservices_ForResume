import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
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
    );
};

export default AuctionListPage;
