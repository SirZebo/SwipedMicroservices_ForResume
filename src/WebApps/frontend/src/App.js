import logo from './logo.svg';
import './App.css';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { LogLevel } from '@microsoft/signalr';
import { useState } from 'react';
import { Button } from 'bootstrap';
import { useEffect } from 'react';
// import { Route } from 'react';
import AuctionById from './components/AuctionById';
import React, { Component } from 'react';
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";

export default function App() {
   
    return (
      <BrowserRouter>
        <Routes>
          <Route path='/auctions/:id' element={<AuctionById/>} />     
        </Routes>
      </BrowserRouter>
    );
  }


const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<App />);

// function App() {
//   // const[conn, setConnection] = useState();
//   // const[bid, setBid] = useState();

//   // const connection = new HubConnectionBuilder()
//   //   .withUrl("https://localhost:6062/bidding/notification/6EC1297B-EC0A-4AA1-BE25-6726E3B51A27")
//   //   .configureLogging(LogLevel.Information)
//   //   .build();

//   // async function start() {
//   //   try {
//   //     await connection.start();
//   //     conn.invoke("GetBid" );

//   //     setConnection(connection);
//   //   } catch(e) {
//   //     console.log(e);
//   //   }
//   // }
  
//   // connection.onclose(async () => {
//   //   await start();
//   // });


      

//   //   // Set up handler
//   //   connection.on("GetBid", (bid) => {
//   //       console.log("send")
//   //       setBid(bid);
//   //   });
      
//   //   connection.on("RecieveBiddingInformation", (bid) => {
//   //     console.log(bid);
//   //     setBid(bid);
//   //   });
  
  
    
//   return (
    
//   );
// }

// export default App;
