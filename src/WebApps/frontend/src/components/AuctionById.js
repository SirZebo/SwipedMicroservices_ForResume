import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { LogLevel } from '@microsoft/signalr';
import { useParams } from "react-router-dom";

function withParams(Component) {
    return props => <Component {...props} params={useParams()} />;
}

class AuctionById extends Component {
    static displayName = AuctionById.name;

    constructor(props) {
        super(props);
        this.state = {
            bid: null,
            connection: null
        };
    }

    componentDidMount() {
        let { id } = this.props.params;

        const newConnection = new HubConnectionBuilder()
            .withUrl("https://localhost:6062/bidding/notification/" + id)
            .configureLogging(LogLevel.Information)
            .build();
        
        this.setState({ connection: newConnection });

        newConnection.start()
            .then(result => {
                console.log('Connected!');
                console.log(id);

                this.state.connection.on("GetBid", (bid) => {
                    console.log(bid);
                    this.setState({ bid });
                });

                this.state.connection.on("SendSseByAuctionId", (bid) => {
                    this.setState({ bid });
                });

                this.state.connection.invoke("GetBid");
            })
            .catch(e => console.log('Connection failed: ', e));
    }

    // Create a new bid
    createBid = async (newBid) => {
        const response = await fetch(`https://localhost:6062/bidding`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newBid)
        });
        const result = await response.json();
        console.log('Bid created:', result);
    };

    // Get bid by ID
    getBidById = async (id) => {
        const response = await fetch(`https://localhost:6062/bidding/${id}`);
        const result = await response.json();
        this.setState({ bid: result });
        console.log('Fetched bid:', result);
    };

    // Update a bid
    updateBid = async (id, updatedBid) => {
        const response = await fetch(`https://localhost:6062/bidding/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedBid)
        });
        const result = await response.json();
        console.log('Bid updated:', result);
    };

    // Delete a bid
    deleteBid = async (id) => {
        const response = await fetch(`https://localhost:6062/bidding/${id}`, {
            method: 'DELETE'
        });
        if (response.ok) {
            console.log(`Bid with ID ${id} deleted`);
            this.setState({ bid: null });
        }
    };

    render() {
        return (
            <div>
                <h2>Bid Details</h2>
                {this.state.bid ? (
                    <div>
                        <p>ID: {this.state.bid.id}</p>
                        <p>Price: {this.state.bid.price}</p>
                        <p>Customer ID: {this.state.bid.customerId}</p>
                        {/* Display more bid details as needed */}
                    </div>
                ) : (
                    <p>No bid data available.</p>
                )}
                {/* Buttons to trigger CRUD actions (for testing) */}
                <button onClick={() => this.createBid({ customerId: "123", auctionId: "456", price: 100 })}>Create Bid</button>
                <button onClick={() => this.getBidById(this.props.params.id)}>Get Bid</button>
                <button onClick={() => this.updateBid(this.props.params.id, { price: 150 })}>Update Bid</button>
                <button onClick={() => this.deleteBid(this.props.params.id)}>Delete Bid</button>
            </div>
        );
    }
}

export default withParams(AuctionById);
