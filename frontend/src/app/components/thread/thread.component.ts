import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RecordCreateModel } from 'src/app/models/record/record-create-model';
import { RecordInfoModel } from 'src/app/models/record/record-info-model';
import { RecordService } from 'src/app/services/record.service';

@Component({
  selector: 'app-thread',
  templateUrl: './thread.component.html',
  styleUrls: ['./thread.component.css']
})
export class ThreadComponent implements OnInit {
  public recordForm: FormGroup;
  public titleControl: FormControl;
  public textControl: FormControl;

  public recordCreateModel: RecordCreateModel = { text: "", title: "", imageBase64: "" }
  private base64textString: string = "";
  public records: RecordInfoModel[] = [];

  constructor(private recordService: RecordService) { }

  ngOnInit() {
    this.recordService.getUserRecord().subscribe((data) => {
      if(data.body) {
        this.records = data.body;
      }
    });

    this.titleControl = new FormControl(this.recordCreateModel.title, [
      Validators.required
    ]);
    this.textControl = new FormControl(this.recordCreateModel.text, [
      Validators.required
    ]);

    this.recordForm = new FormGroup({
      titleControl: this.titleControl,
      textControl: this.textControl
    });
  }

  addRecord() {
    if(this.recordForm.valid) {
      this.recordService.addRecord({ 
        title: this.recordForm.get('titleControl')?.value,
        text: this.recordForm.get('textControl')?.value,
        imageBase64: this.base64textString
      }).subscribe((data) => { 
        if(data.body) {
          this.records.push(data.body);
        }
      });
    }
  }

  handleFileSelect(evt: any) {
    var files = evt.target.files;
    var file = files[0];
  
    if (files && file) {
        var reader = new FileReader();

        reader.onload =this._handleReaderLoaded.bind(this);

        reader.readAsBinaryString(file);
    }
  }

  _handleReaderLoaded(readerEvt: any) {
    var binaryString = readerEvt.target.result;

    this.base64textString= btoa(binaryString);
  }

}
