import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { InviteUserModel } from 'src/app/models/user/invite-user-model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  public inviteFormGroup: FormGroup;
  public inviteEmailControl: FormControl;

  public inviteUserModel: InviteUserModel = { email: "" };

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.inviteEmailControl = new FormControl(this.inviteUserModel.email, [
      Validators.required
    ]);

    this.inviteFormGroup = new FormGroup({
      inviteEmailControl: this.inviteEmailControl
    })
  }

  invite() {
    if(this.inviteFormGroup.valid) {
      this.userService.invite({ email: this.inviteFormGroup.get('inviteEmailControl')?.value })
      .subscribe((data) => {
        if(data.ok) {
          console.log('work');
        }
      });
    }
  }
}
