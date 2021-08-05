import { Result } from "./commonContracts";
import { WeatherForecast, ChatMessage } from "./contracts";
import { safeCommonFetch, safeFetch } from "./helpers";

export class ApiService {
    getWeatherForecast = async () => {
		const result = await safeFetch<WeatherForecast[]>("weatherforecast", {
			method: 'GET'
		})
		return result
	}

	getMessages = async () => {
		const result = await safeFetch<ChatMessage[]>("session/messages", {
			method: 'GET'
		})
		return result
	}

	getBarometer = async () => {
		const result = await safeFetch<number>("session/barometer", {
			method: 'GET'
		})
		return result
	}
	
	sendMessage = async (message: string) => {
		const result = await safeCommonFetch("session/send", {
			method: 'POST',
			body: JSON.stringify({ Text: message }),
			headers: {
				'Content-Type': 'application/json',
				Accept: 'application/json',
			},
		})
		return result
	}
}