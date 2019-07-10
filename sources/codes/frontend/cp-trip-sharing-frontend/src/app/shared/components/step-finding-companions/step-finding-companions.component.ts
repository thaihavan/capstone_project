import { Component, OnInit, Input } from '@angular/core';
import { Schedule } from 'src/app/model/Schedule';

@Component({
  selector: 'app-step-finding-companions',
  templateUrl: './step-finding-companions.component.html',
  styleUrls: ['./step-finding-companions.component.css']
})
export class StepFindingCompanionsComponent implements OnInit {
@Input() schedule: Schedule;
@Input() index: number;
  constructor() { }

  ngOnInit() {
  }

}
