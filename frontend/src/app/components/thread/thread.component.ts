import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-thread',
  templateUrl: './thread.component.html',
  styleUrls: ['./thread.component.css']
})
export class ThreadComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

  openProfile() {
    this.router.navigate(['profile']);
  }
}
