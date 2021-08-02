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
          <Container>
            {this.props.children}
          </Container>
        </BrowserRouter>
      </div>
    );
  }
}
