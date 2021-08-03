import React, { Component } from 'react';
import { BrowserRouter } from 'react-router-dom';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <BrowserRouter>
          <NavMenu />
          <Container style={{ width: "100%", maxWidth: "100%", margin: 0, padding: 0 }}>
            {this.props.children}
          </Container>
        </BrowserRouter>
      </div>
    );
  }
}
