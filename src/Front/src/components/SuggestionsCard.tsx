import React, { Component, useCallback, useEffect, useState } from 'react';
import { Button, Card, message, Space, Spin, Table } from 'antd';
import { Link, useRouteMatch } from "react-router-dom";
import { Chat, PersonalInfo, Suggestions } from '../utilites/api/contracts';
import { ApiService } from '../utilites/api/api';

type SuggestionsProps = {
    chatId: string;
}

const buttonStyle = {
    margin: "0.5rem"
}

const apiService = new ApiService();
 
export const SuggestionsCard: React.FC<SuggestionsProps> = ({ chatId }) => {
    const [ suggestions, setSuggestions ] = useState<Suggestions>();

    const uploadData = async () => {
        const result = await apiService.getSuggestions(chatId);
        if (result.success) {
            await setSuggestions(result.data);
        }
    }
    useEffect(() => {
        setInterval(async () => await uploadData(), 1000);
    }, [chatId]);

    const onClick = (newMessage: string) => async () => {
        const resp = await apiService.sendMessage(chatId, newMessage);
        if (!resp.success) {
            message.error("Error sending message")
        }
    }

    if (!suggestions) {
        return <Spin />
    } else {
        const { messages } = suggestions;
        
        return (<>
            {messages.map((message, i) =>
                <Button key={i} type="primary" style={buttonStyle} onClick={onClick(message)}>{message}</Button>
            )}
        </>);
    }
}
