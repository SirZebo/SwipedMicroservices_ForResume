const express = require('express');
const cors = require('cors');
const amqp = require('amqplib');
const app = express();
const RABBITMQ_URL = process.env.RABBITMQ_URL || 'amqp://guest:guest@localhost:5672';

app.use(cors());
app.use(express.json());

// Set the port
const PORT = process.env.PORT || 5000;

// In-memory data store
let auctions = [
    {
        id: '1',
        title: 'Vintage Watch',
        description: 'A beautiful vintage watch from 1950s.',
        startPrice: 100,
        currentBid: 150,
        bids: [
            { bidder: 'Alice', newBid: 120, timestamp: Date.now() - 60000 },
            { bidder: 'Bob', newBid: 150, timestamp: Date.now() - 30000 },
        ],
        endTime: Date.now() + 600000
    },
    {
        id: '2',
        title: 'Antique Vase',
        description: 'An exquisite antique vase with intricate designs.',
        startPrice: 200,
        currentBid: 250,
        bids: [
            { bidder: 'Charlie', newBid: 220, timestamp: Date.now() - 120000 },
            { bidder: 'Dave', newBid: 250, timestamp: Date.now() - 60000 },
        ],
        endTime: Date.now() + 1200000
    },
];

// SSE clients
let clients = {};

// API to get all auctions
app.get('/api/auctions', (req, res) => {
    res.json(auctions);
});

// API to get a specific auction
app.get('/api/auctions/:id', (req, res) => {
    const auction = auctions.find(a => a.id === req.params.id);
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
app.post('/api/auctions/:id/bids', (req, res) => {
    const auctionId = req.params.id;
    const { bid, bidder } = req.body;

    const auction = auctions.find(a => a.id === auctionId);
    if (!auction) {
        return res.status(404).json({ message: 'Auction not found' });
    }

    if (bid <= auction.currentBid) {
        return res.status(400).json({ message: 'Bid must be higher than current bid' });
    }

    auction.currentBid = bid;
    const newBid = {
        bidder: bidder || 'Anonymous',
        newBid: bid,
        timestamp: Date.now()
    };
    auction.bids.push(newBid);

    if (clients[auctionId]) {
        clients[auctionId].forEach(client => {
            client.write(`data: ${JSON.stringify(newBid)}\n\n`);
        });
    }

    res.status(201).json(newBid);
});

// RabbitMQ Consumer
async function consumeEvents() {
    try {
        const connection = await amqp.connect(RABBITMQ_URL);
        const channel = await connection.createChannel();

        const queues = ['BuildingBlocks.Messaging.Events.BidCreatedIntegrationEvent', 'BuildingBlocks.Messaging.Events.BidUpdatedIntegrationEvent'];

        for (let queue of queues) {
            await channel.assertQueue(queue, { durable: true });

            channel.consume(queue, (message) => {
                if (message !== null) {
                    const event = JSON.parse(message.content.toString());

                    // Expecting properties from IntegrationEvents
                    const { auctionId, newBid, timestamp, bidder } = event;

                    // Process the event data
                    const auction = auctions.find(a => a.id === auctionId);
                    if (auction) {
                        auction.currentBid = newBid;
                        auction.bids.push({
                            bidder: bidder || 'Anonymous',
                            newBid,
                            timestamp: new Date(timestamp)
                        });

                        // Send SSE to clients listening to this auction
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
