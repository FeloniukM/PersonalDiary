import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { RecordInfoModel } from 'src/app/models/record/record-info-model';
import { RecordService } from 'src/app/services/record.service';

@Component({
  selector: 'app-record',
  templateUrl: './record.component.html',
  styleUrls: ['./record.component.css']
})
export class RecordComponent implements OnInit {
  @Input() record: RecordInfoModel;
  @Output() isDeleteRecord: EventEmitter<boolean> = new EventEmitter(false);

  constructor(private recordService: RecordService) { }

  ngOnInit() {
  }

  remove() {
    this.recordService.deleteRecord(this.record.id)
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
