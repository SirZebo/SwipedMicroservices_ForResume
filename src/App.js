import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'; // Use Routes instead of Switch
import AuctionListPage from './pages/AuctionListPage';
import AuctionDetailPage from './pages/AuctionDetailPage';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return (
    <Router>
      <div className="container">
        <Routes>
          <Route exact path="/" element={<AuctionListPage />} />
          <Route path="/auction/:id" element={<AuctionDetailPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;

