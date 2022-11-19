import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { LoginComponent } from './components/login/login.component';
import { MainComponent } from './components/main/main.component';
import { ProfileComponent } from './components/profile/profile.component';
import { RegisterComponent } from './components/register/register.component';
import { ThreadComponent } from './components/thread/thread.component';
import { AuthGuard } from './guards/auth.guard';
import { LoginGuard } from './guards/login.guard';

const routes: Routes = [
  { 
    path: '', 
    component: MainComponent,
    canActivate: [AuthGuard],
    children: [
      { path: 'thread', component: ThreadComponent },
      { path: 'profile', component: ProfileComponent },
    ]
  },
  { 
    path: 'auth', 
    component: AuthComponent,
    canActivate: [LoginGuard], 
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent }
    ] 
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
