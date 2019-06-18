import { Component, OnInit } from '@angular/core';
import { VirtualTrip } from 'src/app/model/VirtualTrip';
import { Post } from 'src/app/model/Post';
import { Author } from 'src/app/model/Author';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-virtual-trips-page',
  templateUrl: './virtual-trips-page.component.html',
  styleUrls: ['./virtual-trips-page.component.css'],
  providers: [DatePipe]
})
export class VirtualTripsPageComponent implements OnInit {
  virtualTrip: VirtualTrip;
  title: string;
  note: string;
  isPublic: boolean;
  post: Post;
  constructor(private datePipe: DatePipe) {
   }

  ngOnInit() {
    this.virtualTrip = new VirtualTrip();
    this.post = new Post();
    this.post.pubDate = this.datePipe.transform( new Date(), 'yyyy-MM-dd hh:mm:ss');
    const author = new Author();
    this.post.author = author;
  }
  addDestination(destinations) {
    this.virtualTrip.virtualTripItems = destinations;
    this.post.title = this.title;
    this.post.isPublic = this.isPublic;
  }
}
