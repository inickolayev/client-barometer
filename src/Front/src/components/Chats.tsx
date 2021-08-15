import React, { Component, useCallback, useEffect, useState } from 'react';
import { Space, Table } from 'antd';
import { Link, useRouteMatch } from "react-router-dom";
import { Chat } from '../utilites/api/contracts';
import { ApiService } from '../utilites/api/api';

const containerStyle = {
    height: "calc(100vh - 90px)",
    width: "100%",
    maxWidth: "100%",
    display: "flex"
}

type ChatsState = {
    chatId: string;
}
const apiService = new ApiService();
 
export const Chats: React.FC = () => {
    const match = useRouteMatch();
    const [ chats, setChats ] = useState<Chat[]>([]);

    const uploadData = async () => {
        const result = await apiService.getChats();
        if (result.success) {
            setChats(result.data);
        }
    }
    useEffect(() => {
        uploadData()
    }, []);
      
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
        },
        {
            title: 'User',
            dataIndex: 'username',
            key: 'username',
            render: (username : any, row : any) => (
                <Space size="middle">
                    <Link to={`${match.url}/${row.id}`}>{username}</Link>
                </Space>
            ),
        }
    ];

    return (<>
        <Table dataSource={chats} columns={columns} />;
    </>);
}
