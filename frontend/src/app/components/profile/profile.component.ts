import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserEmailModel } from 'src/app/models/user/user-email-model';
import { UserInfoModel } from 'src/app/models/user/user-info-model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  public inviteFormGroup: FormGroup;
  public inviteEmailControl: FormControl;

  public changeFormGroup: FormGroup;
  public changeEmailControl: FormControl;

  public inviteUserModel: UserEmailModel = { email: "" };
  public changeRoleModel: UserEmailModel = { email: "" };

  public userInfoModel: UserInfoModel;
  private currentUserId: string | null;

  constructor(private userService: UserService, private router: Router) {
    this.currentUserId = sessionStorage.getItem('id');
  }

  ngOnInit() {
    if(this.currentUserId) {
      this.userService.getUserInfo(this.currentUserId).subscribe((data) => {
        if(data.body) {
          this.userInfoModel = data.body;
        }
      });
    }

    this.inviteEmailControl = new FormControl(this.inviteUserModel.email, [
      Validators.required
    ]);
    this.inviteFormGroup = new FormGroup({
      inviteEmailControl: this.inviteEmailControl
    })

    this.changeEmailControl = new FormControl(this.changeRoleModel.email, [
      Validators.required
    ]);
    this.changeFormGroup = new FormGroup({
      changeEmailControl: this.changeEmailControl
    });
  }

  invite() {
    if(this.inviteFormGroup.valid) {
      this.userService.invite({ email: this.inviteFormGroup.get('inviteEmailControl')?.value })
      .subscribe();
    }
  }

  changeRole() {
    if(this.changeFormGroup.valid) {
      this.userService.changeUserRole({ email: this.changeFormGroup.get('changeEmailControl')?.value })
      .subscribe();
    }
  }

  turnBack() {
    console.log('ss')
    this.router.navigate(['thread']);
  }
}
