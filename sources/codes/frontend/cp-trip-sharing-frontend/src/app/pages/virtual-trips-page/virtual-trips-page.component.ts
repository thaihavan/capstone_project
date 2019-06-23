import { Component, OnInit, AfterViewInit, HostListener, ViewChild } from '@angular/core';
import { VirtualTrip } from 'src/app/model/VirtualTrip';
import { Post } from 'src/app/model/Post';
import { Author } from 'src/app/model/Author';
import { DatePipe } from '@angular/common';
import { MatDialog } from '@angular/material';
import { DialogCreateTripComponent } from './dialog-create-trip/dialog-create-trip.component';
import { VirtualTripService } from 'src/app/core/services/post-service/virtual-trip.service';
import { LoadingScreenComponent } from 'src/app/shared/components/loading-screen/loading-screen.component';

@Component({
  selector: 'app-virtual-trips-page',
  templateUrl: './virtual-trips-page.component.html',
  styleUrls: ['./virtual-trips-page.component.css'],
  providers: [DatePipe]
})
export class VirtualTripsPageComponent implements OnInit, AfterViewInit {
  screenHeight: number;
  isExpandLeft = false;
  expandWidth: number;
  virtualTrip: VirtualTrip;
  title = '';
  note: string;
  isPublic: boolean;
  post: Post;
  coverImage = 'https://material.angular.io/assets/img/examples/shiba2.jpg';
  @ViewChild('leftContent') leftContent;
  constructor(private datePipe: DatePipe, public dialog: MatDialog, private tripService: VirtualTripService) {
   }

  ngOnInit() {
    this.virtualTrip = new VirtualTrip();
    this.post = new Post();
    this.post.pubDate = this.datePipe.transform( new Date(), 'yyyy-MM-dd hh:mm:ss');
    let author = new Author();
    author = JSON.parse(localStorage.getItem('author'));
    this.post.author = author;
    this.isPublic = true;
    this.getScreenSize();
  }
  ngAfterViewInit(): void {
    setTimeout(() => {
      this.openDialog();
    }, 1000);
  }

  @HostListener('window:resize', ['$event'])
    getScreenSize(event?) {
          this.screenHeight = window.innerHeight - 60;
          this.expandWidth = -this.leftContent.nativeElement.clientWidth;
    }

  // add destination from google-map-search
  addDestination(destinations) {
    this.virtualTrip.items = destinations;
  }
  // set policy for virtual post
  setPolicy() {
    this.isPublic = !this.isPublic;
  }
  // expand left conttent
  expandLeft() {
    this.expandWidth = -this.leftContent.nativeElement.clientWidth;
    this.isExpandLeft = !this.isExpandLeft;
  }
  // Open dialog create virtual trips required title
  openDialog() {
    const dialogRef = this.dialog.open(DialogCreateTripComponent, {
      width: '40%',
      data: {
        title: '',
        isPublic: ''
      },
      disableClose: true,
     });
    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        this.title = result.title;
        this.isPublic = result.isPublic;
      }
     });
  }
  // create virtual trip
  createTrip() {
    this.post.title = this.title;
    this.post.isPublic = this.isPublic;
    this.post.image = this.coverImage;
    this.virtualTrip.post = this.post;
    const dialogRef = this.dialog.open(LoadingScreenComponent, {
      width: '100%',
      data: {
        title: '',
        isPublic: ''
      },
      disableClose: true,
     });
    this.tripService.createVirtualTrip(this.virtualTrip).subscribe( result => {
      // tslint:disable-next-line:no-unused-expression
      console.log('create success!');
      dialogRef.close();
    });
  }
}
