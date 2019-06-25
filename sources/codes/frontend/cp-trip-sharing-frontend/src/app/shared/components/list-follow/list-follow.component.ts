import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/model/User';

@Component({
  selector: 'app-list-follow',
  templateUrl: './list-follow.component.html',
  styleUrls: ['./list-follow.component.css']
})
export class ListFollowComponent implements OnInit {
  listUser: User[];
  constructor() { }

  ngOnInit() {
  }

}
