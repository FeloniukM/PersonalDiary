import { Component, Input, OnInit } from '@angular/core';
import { RecordInfoModel } from 'src/app/models/record/record-info-model';

@Component({
  selector: 'app-record',
  templateUrl: './record.component.html',
  styleUrls: ['./record.component.css']
})
export class RecordComponent implements OnInit {
  @Input() record: RecordInfoModel;

  public imagePath: string;
  constructor() { }

  ngOnInit() {
    this.imagePath = `data:image/png;base64, ${this.record.imageBase64}`;
  }

}
