import './App.css';
<<<<<<< Updated upstream
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
=======
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import { Navbar, Nav, Container } from 'react-bootstrap';
>>>>>>> Stashed changes
import AuctionListPage from './pages/AuctionListPage';
import AuctionById from './pages/AuctionById';
import CreateAuctionPage from './pages/CreateAuctionPage';
import EditAuctionPage from './pages/EditAuctionPage';
import BidUpdatePage from './pages/BidUpdatePage';
import ContractsListPage from './pages/ContractsListPage';
import ContractDetailPage from './pages/ContractDetailPage';
<<<<<<< Updated upstream
import React from 'react';
import ReactDOM from 'react-dom/client';
=======
>>>>>>> Stashed changes

export default function App() {
    return (
        <BrowserRouter>
<<<<<<< Updated upstream
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
=======
            <Navbar bg="dark" variant="dark" expand="lg" className="mb-4 shadow-sm">
                <Container>
                    <Navbar.Brand as={Link} to="/" style={{ fontWeight: 'bold', fontSize: '1.2rem', color: '#fff' }}>
                        Swiped! Microservices
                    </Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="ms-auto">
                            <Nav.Link as={Link} to="/" style={{ fontWeight: '500', fontSize: '1rem', color: '#00bfff' }}>
                                Home
                            </Nav.Link>
                            <Nav.Link as={Link} to="/create" style={{ fontWeight: '500', fontSize: '1rem', color: '#f8f9fa' }}>
                                Create Auction
                            </Nav.Link>
                            <Nav.Link as={Link} to="/contracts" style={{ fontWeight: '500', fontSize: '1rem', color: '#5bc0de' }}>
                                Contracts
                            </Nav.Link>
                            <Nav.Link as={Link} to="/update-bid" style={{ fontWeight: '500', fontSize: '1rem', color: '#ffdd57' }}>
                                Update Bid
                            </Nav.Link>
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>

            <Container>
                <Routes>
                    <Route path="/" element={<AuctionListPage />} />
                    <Route path="/auctions/:id" element={<AuctionById />} />
                    <Route path="/create" element={<CreateAuctionPage />} />
                    <Route path="/edit/:id" element={<EditAuctionPage />} />
                    <Route path="/update-bid" element={<BidUpdatePage />} />
                    <Route path="/contracts" element={<ContractsListPage />} />
                    <Route path="/contracts/:id" element={<ContractDetailPage />} />
                </Routes>
            </Container>
        </BrowserRouter>
    );
}
>>>>>>> Stashed changes
