import React, { Component } from 'react';
import { useParams } from 'react-router';
import { ApiService } from '../utilites/api/api';
import { Barometer } from './Barometer';
import { Chat } from './Chat'

const containerStyle = {
    height: "calc(100vh - 90px)",
    width: "100%",
    maxWidth: "100%",
    display: "flex"
}

type ChatPageParams = {
    chatId: string;
}

const dataSource = [
    {
      id: '1',
      source: 'igorrrain',
    }
];
  
const columns = [
    {
      title: 'Id',
      dataIndex: 'id',
      key: 'id',
    },
    {
      title: 'Source',
      dataIndex: 'source',
      key: 'source',
    }
  ];
  

export const ChatPage: React.FC = () => {
    const { chatId } = useParams<ChatPageParams>();
    return (<div style={containerStyle}>
        <Chat chatId={chatId} username="Admin" />
        <Barometer chatId={chatId} />
    </div>);
}
