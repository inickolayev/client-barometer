import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div style={{ margin: "1rem" }}>
        <h1>Добро пожаловать!</h1>
        <p>Вам просто необходимо написать боту <a href="https://telegram.im/@seller_help_bot" target="_blank">@seller_help_bot</a></p>
      </div>
    );
  }
}
