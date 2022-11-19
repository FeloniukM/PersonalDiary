import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { AccessTokenModel } from '../models/token/access-token-model';
import { map } from 'rxjs/operators';
import { HttpResponse } from '@angular/common/http';
import { UserRegisterModel } from '../models/auth/user-register-model';
import { AuthUser } from '../models/auth/auth-user';
import { UserLoginModel } from '../models/auth/user-login-model';
import { BehaviorSubject, Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    public routePrefix = '/api';
    private jwtHelperService = new JwtHelperService();

    private isAuthenticationSub = new BehaviorSubject<boolean>(false);
    public isAuthentication = this.isAuthenticationSub.asObservable();

    constructor(private httpService: HttpInternalService) { }

    public nextisAuthentication(isAuthentication: boolean): void {
        this.isAuthenticationSub.next(isAuthentication);
    }

    public register(user: UserRegisterModel) {
        return this._handleAuthResponse(this.httpService.postFullRequest<AuthUser>(`${this.routePrefix}/auth/register`, user));
    }

    public login(user: UserLoginModel) {
        return this._handleAuthResponse(this.httpService.postFullRequest<AuthUser>(`${this.routePrefix}/auth/login`, user));
    }

    public areTokensExist() {
        const aToken = localStorage.getItem('accessToken');
        if(aToken) {
            return localStorage.getItem('accessToken') && localStorage.getItem('refreshToken') && !this.jwtHelperService.isTokenExpired(aToken);
        }
        return null;
    }

    public revokeRefreshToken() {
        return this.httpService.postFullRequest<AccessTokenModel>(`${this.routePrefix}/token/revoke`, {
            refreshToken: localStorage.getItem('refreshToken')
        });
    }

    public removeTokensFromStorage() {
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
    }

    public refreshTokens() {
        const aToken = localStorage.getItem('accessToken');
        const rToken = localStorage.getItem('refreshToken');

        if(aToken && rToken) {
            return this.httpService
                .postFullRequest<AccessTokenModel>(`${this.routePrefix}/token/refresh`, {
                    accessToken: JSON.parse(aToken),
                    refreshToken: JSON.parse(rToken)
                })
                .pipe(
                    map((resp) => {
                        if(resp.body) {

                            this._setTokens(resp.body);
                            return resp.body;
                        }
                        return null;
                    })
                );
        }
        return null;
    }

    private _handleAuthResponse(observable: Observable<HttpResponse<AuthUser>>) {
        return observable.pipe(
            map((resp) => {
                if(resp.body) {
                    this._setTokens(resp.body.token);
                    sessionStorage.setItem("id", resp.body.user.id);
                    return resp.body.user;
                }
                return null;
            })
        );
    }

    private _setTokens(tokens: AccessTokenModel) {
        if (tokens && tokens.accessToken && tokens.refreshToken) {
            localStorage.setItem('accessToken', tokens.accessToken.token);
            localStorage.setItem('refreshToken', tokens.refreshToken);
        }
    }
}
