import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { UserLoginModel } from 'src/app/models/auth/user-login-model';
import { AuthenticationService } from 'src/app/services/auth.service';
import { CaptchaService } from 'src/app/services/captcha.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {
  public loginForm: FormGroup;
  public emailControl: FormControl;
  public passwordControl: FormControl;

  public captcha: any;
  public captchaAnswer: number;
  public hide: boolean = true;
  public hideCaptcha: boolean = false;

  public userLoginModel: UserLoginModel = { email: "", password: ""};
  private unsubscribe$ = new Subject<void>();

  constructor(private authService: AuthenticationService, 
    private router: Router,
    private captchaService: CaptchaService,
    private sanitizer: DomSanitizer
  ) { }

  ngOnInit(): void {
    this.captchaService.getCaptcha()
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe((data) => {
      if(data.body) {
        this.captcha = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' 
        + data.body.fileContents);
      }
    });

    this.emailControl = new FormControl(this.userLoginModel.email, [
      Validators.required,
      Validators.email,
    ]);
    this.passwordControl = new FormControl(this.userLoginModel.password, [
      Validators.required,
      Validators.minLength(8)
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
      this.authService.login({ 
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

  public verifyCaptcha(): void {  
    this.captchaService.verifyCaptcha(this.captchaAnswer)
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe((data) => {
      if(data.body) {
        this.hideCaptcha = true;
      } 
    });
  }

}
