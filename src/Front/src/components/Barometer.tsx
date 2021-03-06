import React, { Component } from 'react';
import ReactSpeedometer from "react-d3-speedometer"
import { createUseStyles } from 'react-jss';
import { ApiService } from '../utilites/api/api';

type BarometerProps = {
    chatId: string
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
        this.timer = setInterval(async () => await this.loadData(), 1000);
    }

    async loadData() {
        const { chatId } = this.props
        const newBarometer = await this.apiService.getBarometer(chatId);
        if (newBarometer.success) {
            await this.setState({ isLoading: false, barometer: newBarometer.data.value });
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
    height: "13rem",
    display: "flex",
    justifyContent: "center"
};
