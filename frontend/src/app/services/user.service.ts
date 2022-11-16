import { HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { InviteUserModel } from '../models/user/invite-user-model';
import { HttpInternalService } from '../services/http-internal.service';


@Injectable({ providedIn: 'root' })
export class UserService {
    public routePrefix = '/api/user';

    constructor(private httpService: HttpInternalService) {}

    public invite(inviteUserModel: InviteUserModel): Observable<HttpResponse<InviteUserModel>> {
        return this.httpService.postFullRequest(`${this.routePrefix}/invite`, inviteUserModel);
    }

}
