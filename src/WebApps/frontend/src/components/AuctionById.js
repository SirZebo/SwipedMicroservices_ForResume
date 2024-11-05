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

                this.state.connection.on("GetBid", (bid) => {
                    console.log(bid)
                    this.setState({
                        bid: bid
                    });
                });

                this.state.connection.on("SendSseByAuctionId", (bid) => {
                    this.setState({
                        bid: bid
                    });
                });

                this.state.connection.invoke("GetBid");
            })
            .catch(e => console.log('Connection failed: ', e));

    }

    render() {
        return (
            <div></div>
        );
    }
}
export default withParams(AuctionById);
