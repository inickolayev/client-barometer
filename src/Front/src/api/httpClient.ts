import { SessionClient, TelegramBotClient, UserClient } from "./client";
// import { mockApi } from "./mock";
import { binder, fetchWithTimeout, getApiEndpoint } from "./utils";

const httpClient = {
    fetch: (url: RequestInfo, init?: RequestInit) => {
        if (init !== undefined) {
            // TODO: Authorization
            // init.credentials = 'include';
            // const authTokens = authTokensStore.getItem();
            // if (authTokens) {
            //     if (authTokens.access_token) {
            //         init.headers = { ...(init.headers ?? {}), Authorization: `Bearer ${authTokens.access_token}` };
            //     }
            // }
        }

        return fetchWithTimeout(url, init) as Promise<Response>;
    },
};

export const telegramBotClient = binder(new TelegramBotClient(getApiEndpoint(), httpClient), TelegramBotClient);
export const sessionClient = binder(new SessionClient(getApiEndpoint(), httpClient), SessionClient);
export const userClient = binder(new UserClient(getApiEndpoint(), httpClient), UserClient);

// mockApi();
