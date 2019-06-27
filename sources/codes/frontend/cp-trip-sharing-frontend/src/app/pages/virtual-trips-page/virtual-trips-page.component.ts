import { Component, OnInit, AfterViewInit, HostListener, ViewChild } from '@angular/core';
import { VirtualTrip } from 'src/app/model/VirtualTrip';
import { Post } from 'src/app/model/Post';
import { Author } from 'src/app/model/Author';
import { DatePipe } from '@angular/common';
import { MatDialog } from '@angular/material';
import { DialogCreateTripComponent } from './dialog-create-trip/dialog-create-trip.component';
import { VirtualTripService } from 'src/app/core/services/post-service/virtual-trip.service';
import { LoadingScreenComponent } from 'src/app/shared/components/loading-screen/loading-screen.component';
import { UploadImageComponent } from 'src/app/shared/components/upload-image/upload-image.component';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { ActivatedRoute } from '@angular/router';

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
  virtualTripId: string;
  title = '';
  note = '';
  isPublic: boolean;
  readMore = false;
  post: Post;
  urlCoverImage = '';
  isViewDetailTrip: boolean;
  @ViewChild('uploadImage') uploadImage: UploadImageComponent;
  @ViewChild('leftContent') leftContent;
  constructor(private datePipe: DatePipe, public dialog: MatDialog,
              private tripService: VirtualTripService,
              private route: ActivatedRoute) {
   }

  ngOnInit() {
    this.virtualTrip = new VirtualTrip();
    this.virtualTripId = this.route.snapshot.queryParamMap.get('tripId');
    if (this.virtualTripId !== undefined && this.virtualTripId !== null && this.virtualTripId !== '') {
      this.isViewDetailTrip = true;
      this.tripService.getDetailVtrip(this.virtualTripId).subscribe(trip => {
        this.virtualTrip = trip;
        this.isPublic = this.virtualTrip.post.isPublic;
        this.title = this.virtualTrip.post.title;
        this.note = this.virtualTrip.post.content;
        this.urlCoverImage = this.virtualTrip.post.coverImage;
      },
      error => {
        console.log(error);
        alert(error.message);
      });
    }
    this.post = new Post();
    this.post.pubDate = this.datePipe.transform( new Date(), 'yyyy-MM-dd hh:mm:ss');
    let author = new Author();
    author = JSON.parse(localStorage.getItem('author'));
    this.post.author = author;
    this.isPublic = true;
    this.getScreenSize();
  }
  ngAfterViewInit(): void {
    if (!this.isViewDetailTrip) {
      setTimeout(() => {
        this.openDialog('', '' , true , true);
      }, 1000);
    }
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
  openDialog(diaTitle: string, diaNote: string, diaPublic: boolean, disAble: boolean) {
    const dialogRef = this.dialog.open(DialogCreateTripComponent, {
      width: '50%',
      data: {
        title: diaTitle,
        isPublic: diaPublic,
        note: diaNote,
        edit: !disAble,
        save: ''
      },
      disableClose: disAble,
     });
    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined && disAble) {
        this.title = result.title;
        this.isPublic = result.isPublic;
        this.note = result.note;
      }
      if (result !== undefined && !disAble) {
        if (result.save) {
          this.title = result.title;
          this.note = result.note;
          this.virtualTrip.post.title = this.title;
          this.virtualTrip.post.content = this.note;
          if (this.isViewDetailTrip) {
            this.tripService.updateVirtualTrip(this.virtualTrip).subscribe(
              error => {
                alert('update: ' + error);
              }
            );
          }
        }
      }
     });
  }
  // create virtual trip
  createTrip() {
    this.post.title = this.title;
    this.post.isPublic = this.isPublic;
    this.post.coverImage = this.urlCoverImage;
    this.post.content = this.note;
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
      console.log('create success!');
      dialogRef.close();
      this.openDialogMessageConfirm('Bàn đăng đã được tạo!', result.id);
    },
    complete => {
    });
  }

  // open dialog for update
  editAble() {
    this.openDialog(this.title, this.note, this.isPublic, false);
  }

  // event when change image
  fileClick() {
    this.uploadImage.file.nativeElement.click();
  }

  // get image when crop image done
  ImageCropted(image) {
    this.urlCoverImage = image;
  }

  // update virtual trip to server
  saveDestination(des) {
    const foundIndex = this.virtualTrip.items.findIndex(x => x.locationId === des.locationId);
    // tslint:disable-next-line:no-debugger
    debugger;
    this.virtualTrip.items[foundIndex] = des;
    if (this.isViewDetailTrip) {
      this.tripService.updateVirtualTrip(this.virtualTrip).subscribe(
        error => {
          alert(error);
        }
      );
    }
  }
  // open dialog confirm
  openDialogMessageConfirm(message: string, data) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '400px',
      height: '200px',
      position: {
        top: '10px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = message;
    instance.message.url = '/virtual-trips?tripId=' + data;
  }
}
