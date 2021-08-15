import { stringify } from 'querystring';
import React, { Component, useEffect, useState } from 'react';
import { useParams } from 'react-router';
import { ApiService } from '../utilites/api/api';
import { Barometer } from './Barometer';
import { Chat } from './Chat'
import { PersonalInfoCard } from './PersonalInfoCard';
import { SuggestionsCard } from './SuggestionsCard';

const containerStyle = {
    height: "calc(100vh - 90px)",
    width: "100%",
    maxWidth: "100%",
    display: "flex"
}

const secondColumnStyle = {
    display: "flex",
    "flex-direction": "column",
    "align-items": "center",
}

type ChatPageParams = {
    chatId: string;
}

const apiService = new ApiService();

export const ChatPage: React.FC = () => {
    const { chatId } = useParams<ChatPageParams>();
    const [ userId, setUserId ] = useState<string>("");
    
    const uploadData = async () => {
        const result = await apiService.getUser(chatId);
        if (result.success) {
            await setUserId(result.data?.id);
        }
    }
    useEffect(() => {
        uploadData()
    }, [chatId]);

    return (<div style={containerStyle}>
        <Chat chatId={chatId} username="Admin" />
        <div style={secondColumnStyle}>
            <PersonalInfoCard userId={userId} />
            <Barometer chatId={chatId} />
            <SuggestionsCard chatId={chatId} />
        </div>
    </div>);
}
