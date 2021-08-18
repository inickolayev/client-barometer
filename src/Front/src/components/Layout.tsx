import React, { CSSProperties } from "react";
import { BrowserRouter } from "react-router-dom";
import { Container } from "reactstrap";
import { NavMenu } from "./NavMenu";

export const Layout: React.FC = ({ children }) => {
    return (
        <div>
            <BrowserRouter>
                <NavMenu />
                <Container style={containerStyle}>{children}</Container>
            </BrowserRouter>
        </div>
    );
};

const containerStyle: CSSProperties = {
    width: "100%",
    maxWidth: "100%",
    margin: 0,
    padding: 0,
};
