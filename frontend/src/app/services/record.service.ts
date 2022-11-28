import { DatePipe } from '@angular/common';
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

  constructor(private httpService: HttpInternalService, private datePipe: DatePipe) { }

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

  public searchRecordByDate(start: Date, end: Date): Observable<HttpResponse<RecordInfoModel[]>> {
    let transformedStart = this.datePipe.transform(start, 'yyyy-MM-dd');
    let transformedEnd = this.datePipe.transform(end, 'yyyy-MM-dd');
    
    return this.httpService.getFullRequest<RecordInfoModel[]>(`${this.routePrefix}/date/${transformedStart}/${transformedEnd}`);
  }

  public searchRecordByContent(content: string): Observable<HttpResponse<RecordInfoModel[]>> {
    return this.httpService.getFullRequest<RecordInfoModel[]>(`${this.routePrefix}/${content}`);
  }
}
