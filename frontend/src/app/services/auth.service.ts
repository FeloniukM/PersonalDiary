import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { AccessTokenModel } from '../models/token/access-token-model';
import { map } from 'rxjs/operators';
import { HttpResponse } from '@angular/common/http';
import { UserRegisterModel } from '../models/auth/user-register-model';
import { AuthUser } from '../models/auth/auth-user';
import { UserLoginModel } from '../models/auth/user-login-model';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { User } from '../models/user';
import { UserService } from './user.service';
import { EventService } from './event.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    public routePrefix = '/api';
    private user: User;
    private jwtHelperService = new JwtHelperService();

    private isAuthenticationSub = new BehaviorSubject<boolean>(false);
    public isAuthentication = this.isAuthenticationSub.asObservable();

    constructor(private httpService: HttpInternalService, private userService: UserService, private eventService: EventService) { }

    public nextisAuthentication(isAuthentication: boolean): void {
        this.isAuthenticationSub.next(isAuthentication);
    }

    public getUser() {
        return this.user
            ? of(this.user)
            : this.userService.getUserFromToken().pipe(
                map((resp) => {
                    if(resp.body) {
                        this.user = resp.body;
                        this.eventService.userChanged(this.user);
                        return this.user;
                    }
                    return null;
                })
            );
    }

    public setUser(user: User) {
        this.user = user;
        this.eventService.userChanged(user);
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
                    this.user = resp.body.user;
                    this.eventService.userChanged(resp.body.user);
                    return resp.body.user;
                }
                return null;
            })
        );
    }

    private _setTokens(tokens: AccessTokenModel) {
        if (tokens && tokens.accessToken && tokens.refreshToken) {
            localStorage.setItem('accessToken', JSON.stringify(tokens.accessToken.token));
            localStorage.setItem('refreshToken', JSON.stringify(tokens.refreshToken));
            this.getUser();
        }
    }
}
