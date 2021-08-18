import { SendOutlined } from "@ant-design/icons";
import { Button, Input, message, Spin } from "antd";
import dayjs from "dayjs";
import React, { CSSProperties, FormEvent, useCallback, useEffect, useRef, useState } from "react";
import { sessionClient } from "../api/httpClient";
import useApi from "../api/useApi";
import { Bubble } from "./Bubble";

const mainContainerStyle: CSSProperties = {
    width: "100%",
    display: "flex",
    flexDirection: "column",
};

const chatContainerStyle: CSSProperties = {
    width: "100%",
    display: "flex",
    flex: "1 1 0%",
    flexDirection: "column-reverse",
    overflow: "auto",
    paddingLeft: "2.5rem",
    paddingRight: "2.5rem",
    paddingBottom: "1rem",
};

const formStyle: CSSProperties = {
    marginLeft: "auto",
    marginRight: "auto",
    width: "100%",
    display: "flex",
    padding: "2.5rem",
};

const inputGroupStyle: CSSProperties = {
    display: "flex",
};

const inputStyle: CSSProperties = {
    width: "100%",
    borderTopLeftRadius: "0.5rem",
    borderBottomLeftRadius: "0.5rem",
    padding: "0.5rem 0.75rem",
};

const sendButtonStyle: CSSProperties = {
    display: "flex",
    alignItems: "center",
    borderTopRightRadius: "0.5rem",
    borderBottomRightRadius: "0.5rem",
    padding: "1.2rem 1rem",
    marginLeft: -1,
};

const loadingStyle: CSSProperties = {
    display: "flex",
    justifyContent: "space-around",
};

export interface ChatProps {
    username: string;
    chatId: string;
}

export const Chat: React.FC<ChatProps> = ({ chatId, username }) => {
    const [text, setText] = useState<string>("");

    const { firstFetchDone, data, fetch } = useApi({
        initial: [],
        fetchData: sessionClient.messages,
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

    const onSend = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
            await sessionClient.send({ chatId, text });
            setText("");
        } catch (error) {
            message.error("Error sending message");
        }
    };

    const spinner = (
        <div style={loadingStyle}>
            <div>
                <Spin />
                <span> Loading...</span>
            </div>
        </div>
    );

    return (
        <div style={mainContainerStyle}>
            <div style={chatContainerStyle}>
                {!firstFetchDone
                    ? spinner
                    : data.map((item) => (
                          <Bubble
                              right={username === item.username}
                              username={item.username}
                              time={dayjs(item.createdAt).format("HH:mm")}
                              message={item.text}
                              key={item.id}
                          />
                      ))}
                {/* {
                        hasNextPage && (
                            <div className="flex self-center my-2">
                            <a className="ease transition-all delay-75 bg-gray-600 text-center text-sm text-white rounded-full p-2 px-5 cursor-pointer hover:bg-gray-800" onClick={() => fetchNextPage()}>Earlier messages</a>
                            </div>
                            )
                        } */}
            </div>
            <form style={formStyle} onSubmit={onSend}>
                <Input.Group style={inputGroupStyle}>
                    <Input
                        required={true}
                        style={inputStyle}
                        value={text}
                        onChange={({ target: { value } }) => setText(value)}
                        placeholder="Message..."
                    />
                    <Button style={sendButtonStyle} htmlType="submit">
                        Send <SendOutlined />
                    </Button>
                </Input.Group>
            </form>
        </div>
    );
};
