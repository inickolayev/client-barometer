import React, { Component } from 'react';
import { ApiService } from '../utilites/api/api';
import { Barometer } from './Barometer';
import { Chat } from './Chat'

const containerStyle = {
    height: "calc(100vh - 90px)",
    width: "100%",
    maxWidth: "100%",
    display: "flex"
}

export const Session: React.FC = () => {
    return (<div style={containerStyle}>
        <Chat room_id="123" username="Admin" />
        <Barometer />
    </div>);
}
