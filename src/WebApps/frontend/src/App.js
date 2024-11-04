import logo from './logo.svg';
import './App.css';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { LogLevel } from '@microsoft/signalr';
import { useState } from 'react';
import { Button } from 'bootstrap';
import { useEffect } from 'react';

function App() {
  const[conn, setConnection] = useState();
  const[bid, setBid] = useState();

  const connection = new HubConnectionBuilder()
    .withUrl("https://localhost:6062/bidding/notification/6EC1297B-EC0A-4AA1-BE25-6726E3B51A27")
    .configureLogging(LogLevel.Information)
    .build();

  async function start() {
    try {
      await connection.start();
      conn.invoke("GetBid" );

      setConnection(connection);
    } catch(e) {
      console.log(e);
    }
  }
  
  connection.onclose(async () => {
    await start();
  });


      

    // Set up handler
    connection.on("GetBid", (bid) => {
        console.log("send")
        setBid(bid);
    });
      
    connection.on("RecieveBiddingInformation", (bid) => {
      console.log(bid);
      setBid(bid);
    });
  
  
    
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        {!conn ?
          console.log(bid)

        : console.log("not Connected")
        }
        {!conn ?
          () => conn.invoke("GetBid")

        : console.log("not Connected")
        }
        
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
