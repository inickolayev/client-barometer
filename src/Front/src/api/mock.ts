import { PersonalInfo, Suggestions } from "../utilites/api/contracts";
import { BarometerValue, Chat, Message } from "./client";
import { sessionClient, userClient } from "./httpClient";

export function mockApi() {
    sessionClient.chats = async () => {
        const chats: Chat[] = [
            {
                id: "chat1",
                source: "telegram",
                sourceId: "source1",
                username: "i_van",
            },
            {
                id: "chat2",
                source: "telegram",
                sourceId: "source1",
                username: "neo_cat",
            },
        ];
        return chats;
    };

    sessionClient.messages = async (chatId: string) => {
        const messages: Message[] = [
            {
                id: "message1",
                chatId: "chat1",
                userId: "user1",
                username: "i_van",
                createdAt: new Date(2021, 2, 1, 12, 55).toISOString(),
                text: "Текст небольшого сообщения от пользователя",
            },
            {
                id: "message2",
                chatId: "chat1",
                userId: "user1",
                username: "i_van",
                createdAt: new Date(2021, 2, 1, 12, 57).toISOString(),
                text: "Текст второго сообщения",
            },
            {
                id: "message3",
                chatId: "chat1",
                userId: "user2",
                username: "Admin",
                createdAt: new Date(2021, 2, 1, 12, 57).toISOString(),
                text: "Текст ответного сообщения",
            },
        ];
        return messages.reverse();
    };

    sessionClient.barometer = async (chatId: string) => {
        const barometer: BarometerValue = {
            value: 555,
        };
        return barometer;
    };

    sessionClient.suggestions = async (chatId: string) => {
        const suggestions: Suggestions = {
            messages: ["Предложение 1", "Предложение 2"],
        };
        return suggestions;
    };

    sessionClient.user = async () => {
        return {};
    };

    userClient.info = async (userId: string) => {
        const personalInfo: PersonalInfo = {
            name: "Name",
            username: "username",
            age: 23,
        };
        return personalInfo;
    };
}
