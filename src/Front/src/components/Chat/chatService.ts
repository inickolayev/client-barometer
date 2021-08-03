import { InfiniteData, MutationFunction, QueryFunction, QueryKey } from "react-query"
import { queryClient } from "./queryClient";

export interface ChatMessage {
    text: string;
    username: string;
    roomId: string;
    createdAt?: Date;
    id?: string;
}

const PER_PAGE = 20;

const sendMessage: MutationFunction<any, ChatMessage> = async (message) => {
}

const getMessages: QueryFunction<ChatMessage[]> = async (key) => {
    const roomId = key.queryKey[1];
    let date = new Date();
    if (key.pageParam) {
        date = key.pageParam;
    }

    // const snapshot = await db.collection(`Chats/${roomId}/messages`).orderBy("createdAt", "desc").where("createdAt", "<", date).limit(PER_PAGE).get();
    // const retMessage: ChatMessage[] = []
    // for (const message of snapshot.docs) {
    //     const data = message.data() as any;
    //     retMessage.push({ ...data, createdAt: data.createdAt.toDate() })
    // }
    const retMessage : ChatMessage[] = [
        {
            id : "1",
            roomId : "123",
            text : "Hello",
            username : "some",
            createdAt : new Date()
        },
        {
            id : "2",
            roomId : "123",
            text : "World",
            username : "some",
            createdAt : new Date()
        },
        {
            id : "3",
            roomId : "123",
            text : "!",
            username : "some",
            createdAt : new Date()
        },
        
        {
            id : "1",
            roomId : "123",
            text : "Hello",
            username : "some",
            createdAt : new Date()
        },
        {
            id : "2",
            roomId : "123",
            text : "World",
            username : "some",
            createdAt : new Date()
        },
        {
            id : "3",
            roomId : "123",
            text : "!",
            username : "some",
            createdAt : new Date()
        },
        
        {
            id : "1",
            roomId : "123",
            text : "Hello",
            username : "some",
            createdAt : new Date()
        },
        {
            id : "2",
            roomId : "123",
            text : "World",
            username : "some1",
            createdAt : new Date()
        },
        {
            id : "3",
            roomId : "123",
            text : "!",
            username : "some1",
            createdAt : new Date()
        },
        
        {
            id : "1",
            roomId : "123",
            text : "Hello",
            username : "some",
            createdAt : new Date()
        },
        {
            id : "2",
            roomId : "123",
            text : "World",
            username : "some1",
            createdAt : new Date()
        },
    ]
    return retMessage;
}

const hasMessageBefore = async (roomId: string, date?: Date) => {
    return 10;
}

const attachMessageListener = (key: QueryKey): () => void => {
    return () => null;
}

const addMessageToQueryCache = (key: QueryKey, message: ChatMessage) => {
    const cache = queryClient.getQueryData<InfiniteData<ChatMessage[]>>(key);
    const messages = cache?.pages.flat() || [];
    messages.unshift(message);

    const newData: ChatMessage[][] = [];
    for (let i = 0; i < messages.length; i += PER_PAGE) {
        const currentPage = messages.slice(i, i + PER_PAGE);
        newData.push(currentPage);
    }

    queryClient.setQueryData<InfiniteData<ChatMessage[]>>(key, data => {
        return {
            pageParams: data?.pageParams || [],
            pages: newData,
        }
    })
}

export const chatService = {
    sendMessage,
    getMessages,
    attachMessageListener,
    PER_PAGE,
    hasMessageBefore,
}