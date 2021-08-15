import { Result } from "./commonContracts";
import { WeatherForecast, ChatMessage, Chat, User, PersonalInfo, Suggestions, BarometerResult } from "./contracts";
import { safeCommonFetch, safeFetch } from "./helpers";

export class ApiService {
    getWeatherForecast = async () => {
		const result = await safeFetch<WeatherForecast[]>("weatherforecast", {
			method: 'GET'
		})
		return result
	}

	getChats = async () => {
		const result = await safeFetch<Chat[]>("session/chats", {
			method: 'GET'
		})
		return result
	}
	
	getUser = async (chatId: string) => {
		const result = await safeFetch<User>(`session/user?chatId=${chatId}`, {
			method: 'GET'
		})
		return result
	}
	
	getPersonalInfo = async (userId: string) => {
		const result = await safeFetch<PersonalInfo>(`user/info?userId=${userId}`, {
			method: 'GET'
		})
		return result
	}

	getMessages = async (chatId: string) => {
		const result = await safeFetch<ChatMessage[]>(`session/messages?chatId=${chatId}`, {
			method: 'GET'
		})
		return result
	}

	getBarometer = async (chatId: string) => {
		const result = await safeFetch<BarometerResult>(`session/barometer?chatId=${chatId}`, {
			method: 'GET'
		})
		return result
	}

    getSuggestions = async (chatId: string) => {
		const result = await safeFetch<Suggestions>(`session/suggestions?chatId=${chatId}`, {
			method: 'GET'
		})
		return result
    }
	
	sendMessage = async (chatId: string, message: string) => {
		const result = await safeCommonFetch("session/send", {
			method: 'POST',
			body: JSON.stringify({ Text: message, ChatId: chatId }),
			headers: {
				'Content-Type': 'application/json',
				Accept: 'application/json',
			},
		})
		return result
	}
}