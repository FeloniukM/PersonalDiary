import { Injectable } from '@angular/core';

import { Subject } from 'rxjs';
import { UserModel } from '../models/user/user-model';

@Injectable({ providedIn: 'root' })
export class EventService {
    private onUserChanged = new Subject<UserModel>();
    public userChangedEvent$ = this.onUserChanged.asObservable();

    public userChanged(user: UserModel) {
        this.onUserChanged.next(user);
    }
}
