import { message } from "antd";
import React, { CSSProperties, useEffect } from "react";
import { useParams } from "react-router";
import { sessionClient } from "../api/httpClient";
import useApi from "../api/useApi";
import { Barometer } from "./Barometer";
import { Chat } from "./Chat";
import { PersonalInfoCard } from "./PersonalInfoCard";
import { SuggestionsCard } from "./SuggestionsCard";

const containerStyle: CSSProperties = {
    height: "calc(100vh - 90px)",
    width: "100%",
    maxWidth: "100%",
    display: "flex",
};

const secondColumnStyle: CSSProperties = {
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
};

type ChatPageParams = {
    chatId: string;
};

export const ChatPage: React.FC = () => {
    const { chatId } = useParams<ChatPageParams>();

    const { data: user, fetch } = useApi({
        initial: {},
        fetchData: sessionClient.user,
    });

    useEffect(() => {
        fetch(chatId).catch((e) => message.error(e.message));
    }, [chatId, fetch]);

    return (
        <div style={containerStyle}>
            <Chat chatId={chatId} username="Admin" />
            <div style={secondColumnStyle}>
                {user.id && <PersonalInfoCard userId={user.id} />}
                <Barometer chatId={chatId} />
                <SuggestionsCard chatId={chatId} />
            </div>
        </div>
    );
};
