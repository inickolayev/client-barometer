import React from "react";
import { createUseStyles } from "react-jss";

interface BubbleProps {
    right?: boolean;
    username?: string;
    time?: string;
    message?: string;
}

const createStyles = (right?: boolean) =>
    createUseStyles({
        mainContainer: {
            marginTop: "0.5rem",
            display: "flex",
            alignItems: "flex-end",
            flexDirection: right ? "row-reverse" : "row",
        },
        userIcon: {
            backgroundColor: "#2c5282",
            color: "#fff",
            padding: "1rem",
            height: "3rem",
            width: "3rem",
            borderRadius: "9999px",
            fontSize: "5rem",
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
        },
        userIconText: {
            margin: "0",
            fontSize: "2rem",
        },
        messageContainer: {
            padding: "1rem",
            marginTop: "0.5rem",
            marginLeft: "0.5rem",
            marginBottom: "0",
            borderRadius: ".75rem",
            boxShadow: "0 4px 6px -1px rgb(0 0 0 / 10%), 0 2px 4px -1px rgb(0 0 0 / 6%)",
            backgroundColor: right ? "#553c9a" : "#fff",
            borderBottomRightRadius: right ? "0" : ".75rem",
            border: "1px solid #eee",
        },
        message: {
            color: right ? "#fff" : "#a0aec0",
            padding: 0,
            margin: 0,
        },
        time: {
            fontSize: ".875rem",
            marginBottom: "0.25rem",
            marginLeft: "0.25rem",
            color: "#cbd5e0",
            whiteSpace: "nowrap",
        },
    })();

export const Bubble: React.FC<BubbleProps> = ({ right, username, time, message }) => {
    const classes = createStyles(right);
    return (
        <div className={classes.mainContainer}>
            {!right && (
                <div className={classes.userIcon}>
                    <span className={classes.userIconText}>
                        {username?.substr(0, 1).toUpperCase()}
                    </span>
                </div>
            )}
            <div className={classes.messageContainer}>
                <p className={classes.message}>{message}</p>
            </div>
            <span className={classes.time}>{time}</span>
        </div>
    );
};
