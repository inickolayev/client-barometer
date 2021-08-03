import React, { Component } from 'react';
import { ApiService } from '../utilites/api/api';
import { Barometer } from './Barometer';

export const Session: React.FC = () => {
    return (<div>
        Hello from Session component!
        <Barometer />
    </div>);
}
