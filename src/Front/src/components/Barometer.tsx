import { message, Spin } from "antd";
import React, { CSSProperties, useCallback, useEffect, useRef } from "react";
import ReactSpeedometer from "react-d3-speedometer";
import { sessionClient } from "../api/httpClient";
import useApi from "../api/useApi";

const minValue = 0;
const maxValue = 1000;

type BarometerProps = {
    chatId: string;
};

export const Barometer: React.FC<BarometerProps> = ({ chatId }) => {
    const { firstFetchDone, data, fetch } = useApi({
        initial: {},
        fetchData: sessionClient.barometer,
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

    if (!firstFetchDone) {
        return <Spin />;
    }

    return (
        <div style={style}>
            <ReactSpeedometer value={data.value} maxValue={maxValue} minValue={minValue} />
        </div>
    );
};

const style: CSSProperties = {
    width: "40rem",
    height: "13rem",
    display: "flex",
    justifyContent: "center",
};
