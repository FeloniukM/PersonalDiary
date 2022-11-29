import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { RecordCreateModel } from 'src/app/models/record/record-create-model';
import { RecordInfoModel } from 'src/app/models/record/record-info-model';
import { RecordService } from 'src/app/services/record.service';

@Component({
  selector: 'app-thread',
  templateUrl: './thread.component.html',
  styleUrls: ['./thread.component.css']
})
export class ThreadComponent implements OnInit, OnDestroy {
  public recordForm: FormGroup;
  public titleControl: FormControl;
  public textControl: FormControl;
  public fileControl: FormControl;
  public period: FormGroup

  public recordCreateModel: RecordCreateModel = { text: "", title: "", image: null}
  public image: File | null;
  public records: RecordInfoModel[] = [];

  private page: number = 1;
  public hiddenShowMore: boolean = true;
  public searchContent: string;

  private unsubscribe$ = new Subject<void>();

  constructor(private recordService: RecordService) { }

  ngOnInit(): void {
    this.recordService.getUserRecord(this.page)
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe((data) => {
      if(data.body) {
        this.records = data.body;

        if(data.body.length < 5) {
          this.hiddenShowMore = true;
        }
        else {
          this.hiddenShowMore = false
        }
      }
    });

    this.titleControl = new FormControl(this.recordCreateModel.title, [
      Validators.required,
      Validators.maxLength(100)
    ]);
    this.textControl = new FormControl(this.recordCreateModel.text, [
      Validators.required,
      Validators.maxLength(500)
    ]);
    this.fileControl = new FormControl(this.recordCreateModel.image, []);

    this.recordForm = new FormGroup({
      titleControl: this.titleControl,
      textControl: this.textControl,
      fileControl: this.fileControl
    });

    const today = new Date();
    const month = today.getMonth();
    const year = today.getFullYear();

    this.period = new FormGroup({
      start: new FormControl(new Date(year, month, today.getDay())),
      end: new FormControl(new Date(year, month, today.getDay() + 2)),
    });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  addRecord(): void {
    if(this.recordForm.valid) {
      this.recordService.addRecord({ 
        title: this.recordForm.get('titleControl')?.value,
        text: this.recordForm.get('textControl')?.value,
        image: this.image
      })
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((data) => { 
        if(data.body) {
          this.recordForm.reset();
          this.records.unshift(data.body);
        }
      });
    }
  }

  handleFileSelect(evt: any): void {
    var files = evt.target.files;
    var file = files[0];
  
    if (files && file) {
      this.image = file;
    }
  }

  showMore(): void {
    this.page++;

    this.recordService.getUserRecord(this.page)
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe((data) => {
      if(data.body) {
        this.records = this.records.concat(data.body);

        if(data.body.length < 5) {
          this.hiddenShowMore = true;
        }
      }
    });
  }

  sortByDate(): void {
    this.recordService.searchRecordByDate(
      this.period.get('start')?.value, 
      this.period.get('end')?.value)
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe((data) => {
      if(data.body) {
        this.records = data.body;
      }
    })
  }

  sortByContent(): void {
    this.recordService.searchRecordByContent(this.searchContent)
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe((data) => {
      if(data.body) {
        this.records = data.body;
      }
    });
  }

  deleteRecord(id: string, event: boolean): void {
    if(event) {
      let index = this.records.findIndex(x => x.id == id);
      this.records.splice(index, 1);
    }
  }

}
