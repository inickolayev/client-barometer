import React, { Component } from 'react';
import { ApiService } from '../utilites/api/api';
import { WeatherForecast } from '../utilites/api/contracts';

type FetchDataProps = {

}

type FetchDataState = {
    forecasts: WeatherForecast[]
    loading: boolean
}

export class FetchData extends Component<FetchDataProps, FetchDataState> {
    static displayName = FetchData.name;
    _apiService = new ApiService();

    constructor(props: FetchDataProps) {
        super(props);
        this.state = { forecasts: [], loading: true };
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    static renderForecastsTable(forecasts: WeatherForecast[]) {
        return (
        <table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Some date 3</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast =>
                <tr key={new Date(forecast.date).getUTCMilliseconds()}>
                    <td>{new Date(forecast.date).toDateString()}</td>
                    <td>{forecast.temperatureC}</td>
                    <td>{forecast.temperatureF}</td>
                    <td>{forecast.summary}</td>
                </tr>
                )}
            </tbody>
        </table>
        );
    }

    render() {
        let contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : FetchData.renderForecastsTable(this.state.forecasts);

        return (
        <div>
            <h1 id="tabelLabel" >Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
        );
    }

    async populateWeatherData() {
        const response = await this._apiService.getWeatherForecast();
        if (response.success){
            this.setState({ forecasts: response.data, loading: false });
        } else {
            this.setState({ forecasts: [], loading: false });
        }
    }
}
