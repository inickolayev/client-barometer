import React, { Component, useCallback, useEffect, useState } from 'react';
import { Card, Space, Spin, Table } from 'antd';
import { Link, useRouteMatch } from "react-router-dom";
import { Chat, PersonalInfo } from '../utilites/api/contracts';
import { ApiService } from '../utilites/api/api';

type PersonalInfoCardProps = {
    userId: string;
}

const cardStyle = {
    width: "22rem",
    margin: "1rem 1rem 2rem 1rem"
}

const apiService = new ApiService();
 
export const PersonalInfoCard: React.FC<PersonalInfoCardProps> = ({ userId }) => {
    const [ info, setInfo ] = useState<PersonalInfo>();

    const uploadData = async () => {
        const result = await apiService.getPersonalInfo(userId);
        if (result.success) {
            await setInfo(result.data);
        }
    }
    useEffect(() => {
        uploadData()
    }, [userId]);

    if (!info) {
        return <Spin />
    } else {
        const { username, name, age } = info;
        
        return (<>
            <Card title="Personal info" style={cardStyle}>
                <p><b>Username: </b><span>{username}</span></p>
                <p><b>Name: </b><span>{name}</span></p>
                <p><b>Age: </b><span>{age}</span></p>
            </Card>
        </>);
    }
}
