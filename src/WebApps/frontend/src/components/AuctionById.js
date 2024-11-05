import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { LogLevel } from '@microsoft/signalr';
import { useParams } from "react-router-dom";
import {Text, StyleSheet} from 'react-native';
import { useState } from 'react';


function withParams(Component) {
    return props => <Component {...props} params={useParams()} />;
  }

  

class AuctionById extends Component {
    static displayName = AuctionById.name;

    constructor(props) {
        super(props);
        this.state = { bid: null}
    }

    componentDidMount() {
        let { id } = this.props.params;

        const newConnection = new HubConnectionBuilder()
            .withUrl("https://localhost:6062/bidding/notification/" + id)
            .configureLogging(LogLevel.Information)
            .build();
        
        this.setState({
            connection: newConnection
        });

        newConnection.start()
            .then(result => {
                console.log('Connected!');
                console.log(id);

                this.state.connection.on("ReceiveBid", (bid) => {
                    console.log(bid)
                    this.setState({
                        bid: bid
                    });
                });

                this.state.connection.on("SendSseByAuctionId", (bid) => {
                    console.log(bid)
                    this.setState({
                        bid: bid
                    });
                });

                this.state.connection.invoke("GetBid");
            })
            .catch(e => console.log('Connection failed: ', e));

    }

    render() {
        let bid = this.state.bid;
        return (
            <div>
                {
                bid != null
                ? <h1>{this.state.bid.price}</h1>
                : <p>0</p>
            }
                
            </div>
        );
    }
}
export default withParams(AuctionById);
