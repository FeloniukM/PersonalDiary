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
    const data = new FormData();
    data.append('title', record.title);
    data.append('text', record.text);
    
    if(record.image) {
      data.append('image', record.image);
    }

    return this.httpService.postFullRequest<RecordInfoModel>(this.routePrefix, data);
  }

  public getUserRecord(page: number): Observable<HttpResponse<RecordInfoModel[]>> {
    return this.httpService.getFullRequest<RecordInfoModel[]>(`${this.routePrefix}/pageNumber?pageNumber=${page}`);
  }

  public deleteRecord(recordId: string): Observable<void> {
    return this.httpService.deleteRequest(`${this.routePrefix}/${recordId}`)
  }

  public searchRecordByDate(dateWith: Date, dateUndo: Date): Observable<HttpResponse<RecordInfoModel[]>> {
    return this.httpService.getFullRequest<RecordInfoModel[]>(`${this.routePrefix}/date/${dateWith}/${dateUndo}`);
  }

  public searchRecordByContent(content: string): Observable<HttpResponse<RecordInfoModel[]>> {
    return this.httpService.getFullRequest<RecordInfoModel[]>(`${this.routePrefix}/${content}`);
  }
}
