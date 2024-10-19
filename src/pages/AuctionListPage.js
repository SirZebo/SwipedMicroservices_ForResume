import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom'; // You'll need this to link to the AuctionDetailPage

const AuctionListPage = () => {
    const [auctions, setAuctions] = useState([]);

    useEffect(() => {
        // Fetch all auctions
        axios.get('http://localhost:5000/api/auctions')
            .then(response => {
                console.log('Auction list data:', response.data);
                setAuctions(response.data);  // Set the auctions state with the fetched data
            })
            .catch(error => {
                console.error('Error fetching auction list:', error);
            });
    }, []);  // Empty dependency array ensures this runs once when the component mounts

    return (
        <div className="container">
            <h2>Ongoing Auctions</h2>
            <div className="row">
                {auctions.map(auction => (
                    <div className="col-md-4" key={auction.id}>
                        <div className="card mb-4 shadow-sm">
                            <div className="card-body">
                                <h5 className="card-title">{auction.title}</h5>
                                <p className="card-text">{auction.description}</p>
                                <p><strong>Current Bid:</strong> ${auction.currentBid}</p>
                                <Link to={`/auction/${auction.id}`} className="btn btn-primary">
                                    View Auction
                                </Link>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default AuctionListPage;

