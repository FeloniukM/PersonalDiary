import { HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecordCreateModel } from '../models/record/record-create-model';
import { RecordInfoModel } from '../models/record/record-info-model';
import { HttpInternalService } from './http-internal.service';

@Injectable({
  providedIn: 'root'
})
export class RecordService {
  public routePrefix = '/api/record';

  constructor(private httpService: HttpInternalService) { }

  public addRecord(record: RecordCreateModel): Observable<HttpResponse<RecordInfoModel>> {
    return this.httpService.postFullRequest<RecordInfoModel>(this.routePrefix, record);
  }

  public getUserRecord(page: number): Observable<HttpResponse<RecordInfoModel[]>> {
    return this.httpService.getFullRequest<RecordInfoModel[]>(`${this.routePrefix}/pageNumber?pageNumber=${page}`);
  }
}
