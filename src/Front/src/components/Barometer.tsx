import React, { Component } from 'react';
import { ApiService } from '../utilites/api/api';
import ReactSpeedometer from "react-d3-speedometer"

type BarometerProps = {
}

type BarometerState = {
    barometerValue: number;
    inc: number;
}

const minValue = 0;
const maxValue = 1000;

export class Barometer extends Component<BarometerProps, BarometerState> {
    _apiService = new ApiService();
    timer: NodeJS.Timer | null = null
    
    constructor(props: BarometerProps) {
        super(props);
        this.state = { barometerValue: 0, inc: 1 }
        this.timer = setInterval(async () => await this.onUpdateBarometer(), 10);
    }

    componentDidMount() {
    }

    async onUpdateBarometer() {
        const { barometerValue, inc } = this.state;
        const newValue = barometerValue + inc;
        var newInc = inc;
        if (newValue >= 1000) {
            newInc = -1;
        }
        if (newValue <= 0) {
            newInc = 1;
        }
        await this.setState({ barometerValue: newValue, inc: newInc });
    }
    
    render() {
        const { barometerValue } = this.state;
        return (
            <div>
                <ReactSpeedometer value={barometerValue} maxValue={maxValue} minValue={minValue} />
            </div>
        );
    }
}
