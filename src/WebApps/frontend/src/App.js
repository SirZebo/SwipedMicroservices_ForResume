import './App.css';
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import AuctionListPage from './pages/AuctionListPage';
import AuctionById from './pages/AuctionById';
import CreateAuctionPage from './pages/CreateAuctionPage';
import EditAuctionPage from './pages/EditAuctionPage';
import BidUpdatePage from './pages/BidUpdatePage';
import ContractsListPage from './pages/ContractsListPage';
import ContractDetailPage from './pages/ContractDetailPage';
import React from 'react';
import ReactDOM from 'react-dom/client';

export default function App() {
    return (
        <BrowserRouter>
            <nav>
                <Link to="/" className="btn btn-primary">Home</Link>
                <Link to="/create" className="btn btn-secondary">Create Auction</Link>
                <Link to="/bids/update" className="btn btn-secondary">Update Bid</Link>
                <Link to="/contracts" className="btn btn-secondary">Contracts List</Link>
            </nav>
            <Routes>
                {/* Auction routes */}
                <Route path="/" element={<AuctionListPage />} />
                <Route path="/auctions/:id" element={<AuctionById />} />
                <Route path="/create" element={<CreateAuctionPage />} />
                <Route path="/auctions/edit/:id" element={<EditAuctionPage />} />

                {/* Bidding route */}
                <Route path="/bids/update" element={<BidUpdatePage />} />

                {/* Contracts routes */}
                <Route path="/contracts" element={<ContractsListPage />} />
                <Route path="/contracts/:id" element={<ContractDetailPage />} />
            </Routes>
        </BrowserRouter>
    );
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<App />);
