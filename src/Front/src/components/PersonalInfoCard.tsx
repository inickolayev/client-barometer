import { Card, message, Spin } from "antd";
import React, { useEffect } from "react";
import { userClient } from "../api/httpClient";
import useApi from "../api/useApi";

type PersonalInfoCardProps = {
    userId: string;
};

const cardStyle = {
    width: "22rem",
    margin: "1rem 1rem 2rem 1rem",
};


export const PersonalInfoCard: React.FC<PersonalInfoCardProps> = ({ userId }) => {
    const {
        loading,
        data: info,
        fetch,
    } = useApi({
        initial: {},
        fetchData: userClient.info,
    });

    useEffect(() => {
        fetch(userId).catch((e) => message.error(e.message));;
    }, [userId, fetch]);

    if (loading) {
        return <Spin />;
    }

    const { username, name, age } = info;

    return (
        <Card title="Personal info" style={cardStyle}>
            <p>
                <b>Username: </b>
                <span>{username}</span>
            </p>
            <p>
                <b>Name: </b>
                <span>{name}</span>
            </p>
            <p>
                <b>Age: </b>
                <span>{age}</span>
            </p>
        </Card>
    );
};
