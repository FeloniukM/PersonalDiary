import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { RecordInfoModel } from 'src/app/models/record/record-info-model';

@Component({
  selector: 'app-record',
  templateUrl: './record.component.html',
  styleUrls: ['./record.component.css']
})
export class RecordComponent implements OnInit {
  @Input() record: RecordInfoModel;

  public image: any;
  constructor(private sanitizer: DomSanitizer) { }

  ngOnInit() {
    if(this.record.imageBase64) {
      this.image = this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/png;base64, ${this.record.imageBase64}`);
    }
  }

}
