import { message } from "antd";
import React, { useEffect } from "react";
import { telegramBotClient } from "../api/httpClient";
import useApi from "../api/useApi";

export const Home: React.FC = () => {
    const { data, fetch } = useApi({
        initial: {},
        fetchData: telegramBotClient.telegramBotGET,
    });

    useEffect(() => {
        fetch().catch((e) => message.error(e.message));
    }, [fetch]);

    const botName = data?.username ?? "seller_help_bot";

    return (
        <div style={{ margin: "1rem" }}>
            <h1>Добро пожаловать!</h1>
            <p>
                Вам просто необходимо написать боту{" "}
                <a href={`https://telegram.im/@${botName}`} target="_blank" rel="noreferrer">
                    @{botName}
                </a>
            </p>
        </div>
    );
};
