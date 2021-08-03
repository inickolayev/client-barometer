import React from 'react'
import {createUseStyles} from 'react-jss'

interface BubbleProps {
    right?: boolean;
    username?: string;
    time?: string;
    message?: string;
}

const createStyles = (right?: boolean) => createUseStyles({
    mainContainer: { 
        marginTop: "0.5rem",
        marginRight: "0.5rem",
        marginLeft: "0.5rem",
        display: "flex",
        alignItems: "flex-end",
        flexDirection: right ? "row-reverse" : "row"
    },
    userIcon: {
        backgroundColor: "#2c5282",
        color: "#fff",
        padding: "3rem",
        height: "2.5rem",
        width: "2.5rem",
        borderRadius: "9999px",
        fontSize: "5rem",
        display: "flex",
        alignItems: "center",
        justifyContent: "center"
    },
    messageContainer: {
        padding: "1rem",
        margin: "0.5rem",
        marginBottom: "0",
        borderRadius: ".75rem",
        boxShadow: "0 4px 6px -1px rgb(0 0 0 / 10%), 0 2px 4px -1px rgb(0 0 0 / 6%)",
        backgroundColor: right ? "#553c9a" : "#fff",
        borderBottomRightRadius: right ? "0" : ".75rem",
    },
    message: {
        color: right ? "#fff" : "#a0aec0"
    },
    time: {
        fontSize: ".875rem",
        marginBottom: "0.5rem",
        color: "#cbd5e0",
        whiteSpace: "nowrap"
    }
})()

export const Bubble: React.FC<BubbleProps> = ({
    right,
    username,
    time,
    message,
}) => {
    const classes = createStyles(right);
    return (
        <div className={classes.mainContainer}>
            {!right && <div className={classes.userIcon}><p>{username?.substr(0,1).toUpperCase()}</p></div>}
            <div className={classes.messageContainer}>
                <p className={classes.message}>{message}</p>
            </div>
            <p className={classes.time}>{time}</p>
        </div>
    )
}
