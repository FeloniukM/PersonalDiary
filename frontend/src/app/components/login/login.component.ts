import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { UserLoginModel } from 'src/app/models/auth/user-login-model';
import { AuthenticationService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {
  public loginForm: FormGroup;
  public emailControl: FormControl;
  public passwordControl: FormControl;

  public userLoginModel: UserLoginModel = { email: "", password: ""};
  
  private unsubscribe$ = new Subject<void>();

  constructor(private authService: AuthenticationService, private router: Router) {
    
  }

  ngOnInit(): void {
    this.emailControl = new FormControl(this.userLoginModel.email, [
      Validators.required
    ]);
    this.passwordControl = new FormControl(this.userLoginModel.password, [
      Validators.required
    ]);

    this.loginForm = new FormGroup({
      emailControl: this.emailControl,
      passwordControl: this.passwordControl
    });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public signIn(): void {
    if(this.loginForm.valid) {
      this.authService
        .login({ 
          email: this.loginForm.get('emailControl')?.value, 
          password: this.loginForm.get('passwordControl')?.value 
        })
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe(() => {
          this.authService.nextisAuthentication(true);
          this.router.navigate(['thread']);
        });
    }
  }

}
