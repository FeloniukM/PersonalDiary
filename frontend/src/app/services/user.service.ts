import { HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserEmailModel as UserEmailModel } from '../models/user/user-email-model';
import { UserInfoModel } from '../models/user/user-info-model';
import { HttpInternalService } from '../services/http-internal.service';


@Injectable({ providedIn: 'root' })
export class UserService {
    public routePrefix = '/api/user';

    constructor(private httpService: HttpInternalService) {}

    public invite(inviteUserModel: UserEmailModel): Observable<void> {
        return this.httpService.postRequest(`${this.routePrefix}/invite`, inviteUserModel);
    }

    public getUserInfo(userId: string): Observable<HttpResponse<UserInfoModel>> {
        return this.httpService.getFullRequest<UserInfoModel>(`${this.routePrefix}/${userId}`);
    }

    public changeUserRole(inviteUserModel: UserEmailModel): Observable<void> {
        return this.httpService.putRequest(`${this.routePrefix}`, inviteUserModel);
    }

    public deleteUserAccount(): Observable<HttpResponse<void>> {
        return this.httpService.deleteFullRequest<void>(this.routePrefix);
    }

}
