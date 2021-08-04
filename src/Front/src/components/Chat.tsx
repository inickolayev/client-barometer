import dayjs from 'dayjs';
import React from 'react'
import { ChatMessage } from '../utilites/api/contracts';
import { Bubble } from './Bubble'
import { ApiService } from '../utilites/api/api'

export interface ChatProps {
    username: string;
    room_id: string;
}

export interface ChatState {
    messages: ChatMessage[];
    isLoading: boolean;
}


const classes = {
    mainContainer: {
        width: "100%",
        display: "flex",
        flex: "1 1 0%",
        flexDirection: "column-reverse",
        overflow: "auto",
        paddingLeft: "2.5rem",
        paddingRight: "2.5rem"
    },
};

export class Chat extends React.Component<ChatProps, ChatState> {
    apiService = new ApiService();
    timer?: NodeJS.Timer

    constructor(props: ChatProps)
    {
        super(props);
        this.state = { isLoading: true, messages: [] }
        this.loadData();
        this.timer = setInterval(async () => await this.loadData(), 10);
    }

    async loadData() {
        const newMessages = await this.apiService.getMessages();
        if (newMessages.success) {
            await this.setState({ isLoading: false, messages: newMessages.data });
        }
    }

    render() {
        const { isLoading, messages } = this.state;
        const { username } = this.props;

        return (
            <>
                <div style={{
                    width: "100%",
                    display: "flex",
                    flex: "1 1 0%",
                    flexDirection: "column-reverse",
                    overflow: "auto",
                    paddingLeft: "2.5rem",
                    paddingRight: "2.5rem"
                }}>
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
                {/* <form className="mx-auto w-screen flex p-10" onSubmit={handleSendMessage}>
                    <input value={message} className="flex-1 appearance-none border border-transparent w-full py-2 px-4 bg-white text-gray-700 placeholder-gray-400 shadow-md rounded-lg text-base focus:outline-none focus:ring-2 focus:ring-purple-600 focus:border-transparent rounded-r-none" onChange={({ target: { value } }) => setMessage(value)} placeholder="Message..." />
                    <input type="submit" value="Send" className={`transition-all delay-300 ease flex-shrink-0 text-white text-base font-semibold py-2 px-4 rounded-lg shadow-md hover:bg-purple-700 focus:outline-none focus:ring-2 focus:ring-purple-500 focus:ring-offset-2 focus:ring-offset-purple-200 rounded-l-none bg-purple-600`} />
                </form> */}
            </>
        );
    }
}