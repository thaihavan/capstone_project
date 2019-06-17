import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-single-comment',
  templateUrl: './single-comment.component.html',
  styleUrls: ['./single-comment.component.css']
})
export class SingleCommentComponent implements OnInit {
  nameuser = 'Ha Van Thai';
  urlImgavatar = 'https://gody.vn/public/v3/images/bg/br-register.jpg';
  time = '5 phút trước';
  numberlike = '10';
  showRep = false;
  // tslint:disable-next-line:max-line-length
  content = 'This technology was on an episode on Better off Ted about 10 years ago. They found out their department head was a magicians assistant in her spare time.';
  constructor() { }

  ngOnInit() {
  }

  showRepComment() {
    this.showRep = !this.showRep;
  }
}
