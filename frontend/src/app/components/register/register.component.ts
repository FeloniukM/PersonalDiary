import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { UserRegisterModel } from 'src/app/models/auth/user-register-model';
import { AuthenticationService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public registerForm: FormGroup;
  public nameControl: FormControl;
  public emailControl: FormControl;
  public passwordControl: FormControl;

  public userRegisterModel: UserRegisterModel = { nickname: "", email: "", password: ""};
  
  private unsubscribe$ = new Subject<void>();

  constructor(private authService: AuthenticationService, private router: Router) {
  }

  ngOnInit(): void {
    this.emailControl = new FormControl(this.userRegisterModel.email, [
      Validators.required,
      Validators.email
    ]);
    this.passwordControl = new FormControl(this.userRegisterModel.password, [
      Validators.required,
      Validators.minLength(8)
    ]);
    this.nameControl = new FormControl(this.userRegisterModel.nickname, [
      Validators.required
    ]);

    this.registerForm = new FormGroup({
      emailControl: this.emailControl,
      passwordControl: this.passwordControl,
      nameControl: this.nameControl
    });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public signUp(): void {
    if(this.registerForm.valid) {
      this.authService
      .register({ 
        nickname: this.registerForm.get('nameControl')?.value, 
        password: this.registerForm.get('passwordControl')?.value, 
        email: this.registerForm.get('emailControl')?.value
      })
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((data) => {
        this.router.navigate(['main']);
      }); 
    }
  }

}
