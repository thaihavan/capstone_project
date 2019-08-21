import { Component, OnInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-loading-screen',
  templateUrl: './loading-screen.component.html',
  styleUrls: ['./loading-screen.component.css']
})
export class LoadingScreenComponent implements OnInit, OnDestroy {
  constructor() { }

  ngOnInit() {
    document.body.style.overflowY = 'hidden';
  }
  ngOnDestroy(): void {
    document.body.style.overflowY = 'auto';
  }
}
