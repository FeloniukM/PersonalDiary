import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { UserEmailModel } from 'src/app/models/user/user-email-model';
import { UserInfoModel } from 'src/app/models/user/user-info-model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit, OnDestroy {
  public inviteFormGroup: FormGroup;
  public inviteEmailControl: FormControl;

  public changeFormGroup: FormGroup;
  public changeEmailControl: FormControl;

  public inviteUserModel: UserEmailModel = { email: "" };
  public changeRoleModel: UserEmailModel = { email: "" };

  public userInfoModel: UserInfoModel;
  private currentUserId: string | null;

  private unsubscribe$ = new Subject<void>();

  constructor(private userService: UserService, private router: Router) {
    this.currentUserId = sessionStorage.getItem('id');
  }

  ngOnInit(): void {
    if(this.currentUserId) {
      this.userService.getUserInfo(this.currentUserId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((data) => {
        if(data.body) {
          this.userInfoModel = data.body;
        }
      });
    }

    this.inviteEmailControl = new FormControl(this.inviteUserModel.email, [
      Validators.required,
      Validators.email
    ]);
    this.inviteFormGroup = new FormGroup({
      inviteEmailControl: this.inviteEmailControl
    })

    this.changeEmailControl = new FormControl(this.changeRoleModel.email, [
      Validators.required,
      Validators.email
    ]);
    this.changeFormGroup = new FormGroup({
      changeEmailControl: this.changeEmailControl
    });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  invite(): void {
    if(this.inviteFormGroup.valid) {
      this.userService.invite({ email: this.inviteFormGroup.get('inviteEmailControl')?.value })
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe();
    }
  }

  changeRole(): void {
    if(this.changeFormGroup.valid) {
      this.userService.changeUserRole({ email: this.changeFormGroup.get('changeEmailControl')?.value })
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe();
    }
  }

  turnBack(): void {
    this.router.navigate(['thread']);
  }

  deleteUserAccount(): void {
    this.userService.deleteUserAccount()
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe((data) => {
      if(data.ok) {
        alert("The account has been marked as deleted, you can restore the account within two days. To do this, log in again. Otherwise, the account will be deleted");
        localStorage.clear();
        window.location.reload();
      }
    })
  }

}
