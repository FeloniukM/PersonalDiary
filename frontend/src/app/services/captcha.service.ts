import { HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Captcha } from '../models/image/captcha';
import { HttpInternalService } from './http-internal.service';

@Injectable({
  providedIn: 'root'
})
export class CaptchaService {
  public routePrefix = '/api/captcha';


  constructor(private httpService: HttpInternalService) { }
  
  public getCaptcha(): Observable<HttpResponse<Captcha>> {
    return this.httpService.getFullRequest<Captcha>(this.routePrefix);
  }

  public verifyCaptcha(answer: number): Observable<HttpResponse<boolean>> {
    return this.httpService.getFullRequest<boolean>(`${this.routePrefix}/verify/${answer}`)
  }

}
