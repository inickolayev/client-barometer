import { Result } from "./commonContracts";
import { WeatherForecast } from "./contracts";
import { safeFetch } from "./helpers";

export class ApiService {
    getWeatherForecast = async () => {
		const result = await safeFetch<WeatherForecast[]>("weatherforecast", {
			method: 'GET'
		})
		return result
	}
}