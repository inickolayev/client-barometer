import React, { Component } from 'react';
import ReactSpeedometer from "react-d3-speedometer"
import { createUseStyles } from 'react-jss';
import { ApiService } from '../utilites/api/api';

type BarometerProps = {
}

type BarometerState = {
    barometer: number;
    isLoading: boolean;
}

const minValue = 0;
const maxValue = 1000;

export class Barometer extends Component<BarometerProps, BarometerState> {
    apiService = new ApiService();
    timer?: NodeJS.Timer

    constructor(props: BarometerProps)
    {
        super(props);
        this.state = { isLoading: true, barometer: 0 }
        this.loadData();
        this.timer = setInterval(async () => await this.loadData(), 10);
    }

    async loadData() {
        const newBarometer = await this.apiService.getBarometer();
        if (newBarometer.success) {
            await this.setState({ isLoading: false, barometer: newBarometer.data });
        }
    }

    render() {
        const { isLoading, barometer } = this.state;
        return (
            <div style={style}>
                <ReactSpeedometer value={barometer} maxValue={maxValue} minValue={minValue} />
            </div>
        );
    }
}

const style = {
    width: "40rem",
    display: "flex",
    justifyContent: "center"
};
