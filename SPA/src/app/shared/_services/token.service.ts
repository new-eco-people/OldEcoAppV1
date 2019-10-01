import { AppToken } from './../../core/app-domain/app-token';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  tokenHelper = new JwtHelperService();

  constructor() { }

  save(token = null, user = null) {
    if (token) {
      localStorage.setItem('token', token);
    }

    if (user) {
      localStorage.setItem('user', JSON.stringify(user));
    }
  }

  getUserAsObject() {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
  }

  getToken() {
    const token = localStorage.getItem('token');
    return token ? token : null;
  }

  clear() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }

  getTokenAsObject() {
    const token = localStorage.getItem('token');
    return token ? {asString: token, isExpired: this.tokenHelper.isTokenExpired(token), ...this.decode(token)} : null;
  }

  decode(token: string): AppToken {
    return this.tokenHelper.decodeToken(token);
  }
}
