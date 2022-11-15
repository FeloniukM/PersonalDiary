import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { HttpInternalService } from '../services/http-internal.service';


@Injectable({ providedIn: 'root' })
export class UserService {
    public routePrefix = '/api/users';

    constructor(private httpService: HttpInternalService) {}

    public getUserFromToken() {
        return this.httpService.getFullRequest<User>(`${this.routePrefix}/fromToken`);
    }

    public getUserById(id: number) {
        return this.httpService.getFullRequest<User>(`${this.routePrefix}`, { id });
    }
}
