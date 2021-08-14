import dayjs from 'dayjs';
import React, { CSSProperties, FormEvent } from 'react'
import { ChatMessage } from '../utilites/api/contracts';
import { Bubble } from './Bubble'
import { ApiService } from '../utilites/api/api'
import { Input, Button, message } from 'antd'
import { SendOutlined } from '@ant-design/icons';

export interface ChatProps {
    username: string;
    chatId: string;
}

export interface ChatState {
    messages: ChatMessage[];
    isLoading: boolean;
    newMessage: string;
}

const mainContainerStyle :  CSSProperties = {
    width: "100%",
    display: "flex",
    flexDirection: "column"
}

const chatContainerStyle : CSSProperties = {
    width: "100%",
    display: "flex",
    flex: "1 1 0%",
    flexDirection: 'column-reverse',
    overflow: "auto",
    paddingLeft: "2.5rem",
    paddingRight: "2.5rem",
    paddingBottom: "1rem"
};

const formStyle = {
    marginLeft: "auto",
    marginRight: "auto",
    width: "100%",
    display: "flex",
    padding: "2.5rem",
}

const inputStyle = {
    width: "100%",
}

const sendButtonStyle = {
    display: "flex",
    alignItems: "center"
}

export class Chat extends React.Component<ChatProps, ChatState> {
    apiService = new ApiService();
    timer?: NodeJS.Timer

    constructor(props: ChatProps)
    {
        super(props);
        this.state = { isLoading: true, messages: [], newMessage: "" }
        this.loadData();
        this.timer = setInterval(async () => await this.loadData(), 1000);
    }

    async loadData() {
        const { chatId } = this.props;
        const newMessages = await this.apiService.getMessages(chatId);
        if (newMessages.success) {
            await this.setState({ isLoading: false, messages: newMessages.data });
        }
    }

    async onSend(e: FormEvent<HTMLFormElement>) {
        e.preventDefault();
        const { chatId } = this.props;
        const { newMessage } = this.state;
        const resp = await this.apiService.sendMessage(chatId, this.state.newMessage);
        if (resp.success) {
            this.setState({ newMessage: "" })
        } else {
            message.error("Error sending message")
        }
    }

    onMessageChange(message: string) {
        this.setState({ newMessage: message });
    }

    render() {
        const { isLoading, messages, newMessage } = this.state;
        const { username } = this.props;

        return (
            <div style={mainContainerStyle}>
                <div style={chatContainerStyle}>
                    {
                        isLoading
                        ? "loading..."
                        : messages.map((data) =>
                            <Bubble right={username === data.username}
                                username={data.username}
                                time={dayjs(data.createdAt).format("HH:mm")}
                                message={data.text}
                                key={data.id}
                            />
                        )
                    }
                    {/* {
                        hasNextPage && (
                            <div className="flex self-center my-2">
                            <a className="ease transition-all delay-75 bg-gray-600 text-center text-sm text-white rounded-full p-2 px-5 cursor-pointer hover:bg-gray-800" onClick={() => fetchNextPage()}>Earlier messages</a>
                            </div>
                            )
                        } */}
                </div>
                <form style={formStyle} onSubmit={(e) => this.onSend(e)}>
                    <Input
                        required={true}
                        style={inputStyle}
                        value={newMessage}
                        onChange={({ target: { value } }) => this.onMessageChange(value)}
                        placeholder="Message..."
                    />
                    <Button style={sendButtonStyle} htmlType="submit" icon={<SendOutlined/>} >Send</Button>
                </form>
            </div>
        );
    }
}