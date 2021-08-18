import { message, Space, Table } from "antd";
import { ColumnsType } from "antd/lib/table";
import React, { useEffect } from "react";
import { Link, useHistory, useRouteMatch } from "react-router-dom";
import { Chat } from "../api/client";
import { sessionClient } from "../api/httpClient";
import useApi from "../api/useApi";

export const Chats: React.FC = () => {
    const match = useRouteMatch();
    const history = useHistory();

    const { loading, data, fetch } = useApi({
        initial: [],
        fetchData: sessionClient.chats,
    });

    useEffect(() => {
        fetch().catch((e) => message.error(e.message));;
    }, [fetch]);

    const columns: ColumnsType<Chat> = [
        {
            title: "Id",
            dataIndex: "id",
            key: "id",
        },
        {
            title: "Source",
            dataIndex: "source",
            key: "source",
        },
        {
            title: "User",
            dataIndex: "username",
            key: "username",
            render: (username: string, chat) => (
                <Space size="middle">
                    <Link to={`${match.url}/${chat.id}`}>@{username}</Link>
                </Space>
            ),
        },
    ];

    return (
        <Table
            dataSource={data}
            columns={columns}
            loading={loading}
            onRow={(chat) => ({
                style: { cursor: "pointer" },
                onClick: () => history.push(`${match.url}/${chat.id}`),
            })}
        />
    );
};
