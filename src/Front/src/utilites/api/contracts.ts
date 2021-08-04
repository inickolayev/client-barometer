export type WeatherForecast = {
    date: string
    temperatureC: number
    temperatureF: number
    summary: string
}

export type ChatMessage = {
    text: string;
    username: string;
    roomId: string;
    createdAt?: Date;
    id?: string;
}
