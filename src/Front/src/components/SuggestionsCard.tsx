import { Button, message, Spin } from "antd";
import React, { useCallback, useEffect, useRef } from "react";
import { sessionClient } from "../api/httpClient";
import useApi from "../api/useApi";

type SuggestionsProps = {
    chatId: string;
};

const buttonStyle = {
    margin: "0.5rem",
};

export const SuggestionsCard: React.FC<SuggestionsProps> = ({ chatId }) => {
    const {
        firstFetchDone,
        data: suggestions,
        fetch,
    } = useApi({
        initial: {},
        fetchData: sessionClient.suggestions,
    });

    const timer = useRef<NodeJS.Timer>();

    const fetchMore = useCallback(async () => {
        if (timer.current) {
            clearInterval(timer.current);
        }
        try {
            await fetch(chatId);
        } catch (e) {
            message.error(e.message);
        }
        timer.current = setInterval(fetchMore, 3000);
    }, [chatId, fetch]);

    useEffect(() => {
        fetchMore();
    }, [fetchMore]);

    const onClick = (text: string) => async () => {
        try {
            await sessionClient.send({
                chatId,
                text,
            });
        } catch (error) {
            message.error("Error sending message");
        }
    };

    if (!firstFetchDone) {
        return <Spin />;
    }

    const { messages } = suggestions;

    return (
        <>
            {messages?.map((message, index) => (
                <Button key={index} type="primary" style={buttonStyle} onClick={onClick(message)}>
                    {message}
                </Button>
            ))}
        </>
    );
};
