import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  constructor(private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    if (!this.route.snapshot.firstChild) {
      this.router.navigate(['thread'], { relativeTo: this.route, replaceUrl: true });
    }
  }

  openProfile() {
    this.router.navigate(['profile']);
  }
}
