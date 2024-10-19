import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';

const AuctionDetailPage = () => {
    const { id } = useParams();  // Capture the auction ID from the URL
    const [auction, setAuction] = useState({});
    const [bids, setBids] = useState([]);

    useEffect(() => {
        if (!id) {
            console.error("No auction ID found in URL");
            return;
        }

        // Fetch the initial auction details
        axios.get(`http://localhost:5000/api/auctions/${id}`)
            .then(response => {
                console.log('Auction data:', response.data);
                setAuction(response.data);
                setBids(response.data.bids || []);
            })
            .catch(error => {
                console.error('Error fetching auction details:', error);
            });

        // Set up SSE for real-time bid updates
        const eventSource = new EventSource(`http://localhost:5000/api/auctions/${id}/bids/updates`);
        eventSource.onmessage = (event) => {
            console.log('Received bid update:', event.data);  // Log incoming bid update
            const newBid = JSON.parse(event.data);  // Parse event data to newBid
            setBids(prevBids => [...prevBids, newBid]);  // Add the new bid to bid history
            setAuction(prevAuction => ({ ...prevAuction, currentBid: newBid.newBid }));  // Update current bid
        };

        // Clean up SSE when component unmounts
        return () => {
            eventSource.close();
        };
    }, [id]);

    return (
        <div className="container">
            <h1>{auction.title || 'Loading...'}</h1>
            <p>{auction.description || 'No description available'}</p>
            <p><strong>Current Bid:</strong> ${auction.currentBid || 'N/A'}</p>

            <h3>Bid History</h3>
            <ul>
                {bids.length > 0 ? (
                    bids.map((bid, index) => (
                        <li key={index}>
                            <strong>{bid.bidder}:</strong> ${bid.newBid} at {new Date(bid.timestamp).toLocaleTimeString()}
                        </li>
                    ))
                ) : (
                    <li>No bids yet</li>
                )}
            </ul>
        </div>
    );
};

export default AuctionDetailPage;
