import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { RecordInfoModel } from 'src/app/models/record/record-info-model';
import { RecordService } from 'src/app/services/record.service';

@Component({
  selector: 'app-record',
  templateUrl: './record.component.html',
  styleUrls: ['./record.component.css']
})
export class RecordComponent implements OnInit, OnDestroy {
  @Input() record: RecordInfoModel;
  @Output() isDeleteRecord: EventEmitter<boolean> = new EventEmitter(false);

  private unsubscribe$ = new Subject<void>();

  constructor(private recordService: RecordService) { }

  ngOnInit(): void { }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  remove(): void {
    this.recordService.deleteRecord(this.record.id)
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe(() => {
      this.isDeleteRecord.next(true);
    });
  }

  canBeDeleted(): boolean {
    let currentDate = new Date();
    let createdAt = new Date(this.record.createdAt)

    return ((currentDate.getTime() - createdAt.getTime()) / (1000 * 3600 * 24)) > 2;
  }

}
