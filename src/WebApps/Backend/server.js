const express = require('express');
const cors = require('cors');
const amqp = require('amqplib');
const axios = require('axios'); // To make HTTP requests to the auction service
const app = express();
const RABBITMQ_URL = process.env.RABBITMQ_URL || 'amqp://guest:guest@localhost:5672';
const AUCTION_SERVICE_URL = 'http://localhost:6060/auctions'; // Change this if your auction service has a different URL

app.use(cors());
app.use(express.json());

// Set the port
const PORT = process.env.PORT || 5000;

// SSE clients
let clients = {};

// Helper function to fetch auctions from the auction service
async function fetchAuctions() {
    try {
        const response = await axios.get(AUCTION_SERVICE_URL);
        return response.data;
    } catch (error) {
        console.error('Error fetching auctions:', error);
        return [];
    }
}

// Helper function to fetch a single auction by ID from the auction service
async function fetchAuctionById(id) {
    try {
        const response = await axios.get(`${AUCTION_SERVICE_URL}/${id}`);
        return response.data;
    } catch (error) {
        console.error(`Error fetching auction with ID ${id}:`, error);
        return null;
    }
}

// API to get all auctions
app.get('/api/auctions', async (req, res) => {
    const auctions = await fetchAuctions();
    res.json(auctions);
});

// API to get a specific auction
app.get('/api/auctions/:id', async (req, res) => {
    const auction = await fetchAuctionById(req.params.id);
    if (auction) {
        res.json(auction);
    } else {
        res.status(404).json({ message: 'Auction not found' });
    }
});

// SSE endpoint for bid updates
app.get('/api/auctions/:id/bids/updates', (req, res) => {
    const auctionId = req.params.id;
    res.setHeader('Content-Type', 'text/event-stream');
    res.setHeader('Cache-Control', 'no-cache');
    res.setHeader('Connection', 'keep-alive');
    res.flushHeaders();

    if (!clients[auctionId]) {
        clients[auctionId] = [];
    }
    clients[auctionId].push(res);

    req.on('close', () => {
        clients[auctionId] = clients[auctionId].filter(client => client !== res);
    });
});

// API to place a bid
app.post('/api/auctions/:id/bids', async (req, res) => {
    const auctionId = req.params.id;
    const { bid, bidder } = req.body;

    // Fetch auction details from the auction service
    const auction = await fetchAuctionById(auctionId);
    if (!auction) {
        return res.status(404).json({ message: 'Auction not found' });
    }

    if (bid <= auction.currentBid) {
        return res.status(400).json({ message: 'Bid must be higher than the current bid' });
    }

    const newBid = {
        bidder: bidder || 'Anonymous',
        newBid: bid,
        timestamp: Date.now()
    };

    // Notify clients via SSE
    if (clients[auctionId]) {
        clients[auctionId].forEach(client => {
            client.write(`data: ${JSON.stringify(newBid)}\n\n`);
        });
    }

    // Send the bid to the auction service for persistence
    try {
        await axios.post(`${AUCTION_SERVICE_URL}/${auctionId}/bids`, newBid);
        res.status(201).json(newBid);
    } catch (error) {
        console.error('Error placing bid:', error);
        res.status(500).json({ message: 'Error placing bid' });
    }
});

// RabbitMQ Consumer
async function consumeEvents() {
    try {
        const connection = await amqp.connect(RABBITMQ_URL);
        const channel = await connection.createChannel();

        const queues = ['BuildingBlocks.Messaging.Events.BidCreatedIntegrationEvent', 'BuildingBlocks.Messaging.Events.BidUpdatedIntegrationEvent'];

        for (let queue of queues) {
            await channel.assertQueue(queue, { durable: true });

            channel.consume(queue, async (message) => {
                if (message !== null) {
                    const event = JSON.parse(message.content.toString());

                    const { auctionId, newBid, timestamp, bidder } = event;
                    const auction = await fetchAuctionById(auctionId);

                    if (auction) {
                        auction.currentBid = newBid;
                        auction.bids.push({
                            bidder: bidder || 'Anonymous',
                            newBid,
                            timestamp: new Date(timestamp)
                        });

                        // Notify clients listening for this auction
                        if (clients[auctionId]) {
                            clients[auctionId].forEach(client => {
                                client.write(`data: ${JSON.stringify(event)}\n\n`);
                            });
                        }
                    }

                    channel.ack(message);
                }
            });
        }
    } catch (error) {
        console.error('Error consuming events:', error);
    }
}

// Start the RabbitMQ consumer
consumeEvents();

// Start the server
app.listen(PORT, () => {
    console.log(`Server running on http://localhost:${PORT}`);
});
