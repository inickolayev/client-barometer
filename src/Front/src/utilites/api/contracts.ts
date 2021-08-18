export type WeatherForecast = {
    date: string
    temperatureC: number
    temperatureF: number
    summary: string
}

export type ChatMessage = {
    id: string;
    text: string;
    username: string;
    chatId: string;
    userId: string;
    createdAt?: Date;
}

export type Chat = {
    id: string;
    sourceId: string;
    source: string;
    username: string;
}

export type BarometerResult = {
    value: number;
}

export type Suggestions = {
    messages: string[];
}

export type User = {
    id: string;
    sourceId: string;
    source: string;
}

export type PersonalInfo = {
    username: string;
    name: string;
    age: number;
}
