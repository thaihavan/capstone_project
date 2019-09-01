import {
  Component,
  OnInit,
  AfterViewInit,
  HostListener,
  ViewChild
} from '@angular/core';
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
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { GlobalErrorHandler } from 'src/app/core/globals/GlobalErrorHandler';
import { AlertifyService } from 'src/app/core/services/alertify-service/alertify.service';

@Component({
  selector: 'app-virtual-trips-page',
  templateUrl: './virtual-trips-page.component.html',
  styleUrls: ['./virtual-trips-page.component.css'],
  providers: [DatePipe]
})
export class VirtualTripsPageComponent implements OnInit, AfterViewInit {
  screenHeight: number;
  expandWidth: number;

  virtualTripId = '';
  title = '';
  note = '';
  urlCoverImage = '';
  userRole = '';

  isPublic: boolean;
  readMore = false;
  isExpandLeft = false;
  isViewDetailTrip: boolean;

  virtualTrip: VirtualTrip;
  post: Post;
  author: Author;
  @ViewChild('uploadImage') uploadImage: UploadImageComponent;
  @ViewChild('leftContent') leftContent;
  constructor(
    private datePipe: DatePipe,
    public dialog: MatDialog,
    private tripService: VirtualTripService,
    private route: ActivatedRoute,
    private titleService: Title,
    private errorHandler: GlobalErrorHandler,
    private alertify: AlertifyService,
    private router: Router
  ) {
    this.titleService.setTitle('Chuyến đi');
  }

  ngOnInit() {
    this.virtualTrip = new VirtualTrip();
    // check is view detail?
    this.virtualTripId = this.route.snapshot.paramMap.get('tripId');
    if (
      this.virtualTripId === undefined ||
      this.virtualTripId === null ||
      this.virtualTripId === ''
    ) {
      this.preCreate();
      // this.openDialog('', '', true, true);
    } else {
      this.isViewDetailTrip = true;
      this.getVirtualTrip();
    }

    this.getScreenSize();
  }
  ngAfterViewInit(): void {
    if (!this.isViewDetailTrip) {
      setTimeout(() => {
        this.openDialog('', '', true, true);
      }, 500);
    }
  }

  // set resize google map and expand left menu
  @HostListener('window:resize', ['$event'])
  getScreenSize(event?) {
    this.screenHeight = window.innerHeight - 60;
    this.expandWidth = -this.leftContent.nativeElement.clientWidth;
  }

  // getVirtual by id
  getVirtualTrip() {
    this.tripService.getDetailVtrip(this.virtualTripId).subscribe(
      trip => {
        this.virtualTrip = trip;
        this.isPublic = this.virtualTrip.post.isPublic;
        this.title = this.virtualTrip.post.title;
        this.note = this.virtualTrip.post.content;
        this.urlCoverImage = this.virtualTrip.post.coverImage;
        this.author = this.virtualTrip.post.author;
        this.userRole = this.checkRoleUser();
      },
      this.errorHandler.handleError
    );
  }

  // pre create virtual trip
  preCreate() {
    // set variable if is create
    this.post = new Post();
    this.post.pubDate = this.datePipe.transform(
      new Date(),
      'yyyy-MM-dd hh:mm:ss'
    );
    if (localStorage.getItem('Token') !== null) {
      this.author = new Author();
      this.post.author = this.author;
      this.userRole = 'author';
    }
    this.isPublic = true;
  }

  // check role user
  checkRoleUser() {
    const user = JSON.parse(localStorage.getItem('User'));
    if (user === null) {
      return 'guest';
    }
    if (user.id === this.author.id) {
    return 'author';
    }
    return 'member';
  }

  // add destination from google-map-search
  addDestination(destinations) {
    if (
      this.virtualTrip.items === undefined ||
      this.virtualTrip.items === null
    ) {
      this.virtualTrip.items = destinations;
    } else {
      this.virtualTrip.items.push(destinations.pop());
      this.sendUpdateRequest();
    }
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
  openDialog(
    diaTitle: string,
    diaNote: string,
    diaPublic: boolean,
    disAble: boolean
  ) {
    const dialogRef = this.dialog.open(DialogCreateTripComponent, {
      width: '50%',
      data: {
        title: diaTitle,
        isPublic: diaPublic,
        note: diaNote,
        edit: !disAble,
        save: ''
      },
      disableClose: disAble
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
          this.sendUpdateRequest();
        }
      }
    });
  }

  // create virtual trip
  createTrip() {
    if (this.urlCoverImage === '') {
      this.alertify.error('Yêu cầu ảnh bìa cho bài viết');
      return;
    }
    if (this.virtualTrip.items.length === 0) {
      this.alertify.error('Yêu cầu nhập địa điểm của chuyến đi!');
      return;
    }
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
      disableClose: true
    });
    this.tripService.createVirtualTrip(this.virtualTrip).subscribe(
      result => {
        dialogRef.close();
        this.openDialogMessageConfirm('Bàn đăng đã được tạo!', result.id, 'success', false);
      },
      this.errorHandler.handleError,
      () => {
      }
    );
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
    if (this.isViewDetailTrip) {
      this.virtualTrip.post.coverImage = this.urlCoverImage;
      this.sendUpdateRequest();
    }
  }

  // update virtual trip to server
  saveDestination(des) {
    if (des.typeAction === 'remove') {
      this.virtualTrip.items = this.virtualTrip.items.filter(
        item => item.locationId !== des.item.locationId
      );
      this.alertify.success('Đã xóa địa điểm');
    } else {
      const foundIndex = this.virtualTrip.items.findIndex(
        x => x.locationId === des.item.locationId
      );
      this.virtualTrip.items[foundIndex] = des.item;
    }
    this.sendUpdateRequest();
  }


  openDialogMessageConfirm(message: string, url: string, messageType: string, remove: boolean) {
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '500px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: messageType === 'success' ? true : false
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = message;
    instance.message.messageType = messageType;
    if (messageType === 'success') {
      if (remove) {
        instance.message.url = '/user/' + this.author.id + url;
      } else {
        instance.message.url = '/chuyen-di/' + url;
      }
    }

    dialogRef.afterClosed().subscribe(res => {
      if (res === 'continue') {
        this.tripService.deletePost(this.virtualTripId).subscribe(
          respone => { },
        (error) => {
          this.alertify.error('Lỗi xóa bài viết');
        },
        () => {
          // this.alertify.success('Xóa bài viết thành công');
          this.openDialogMessageConfirm('Bài viết đã được xóa!', '', 'success', true);
        }
        );
      }
    });
  }

  removeTrip() {
    this.openDialogMessageConfirm('Bạn có muốn xóa bài viết?', this.author.id, 'confirm', true);
  }

  // send update request to server
  sendUpdateRequest() {
    if (this.isViewDetailTrip) {
      this.tripService.updateVirtualTrip(this.virtualTrip).subscribe(
        res => {},
        this.errorHandler.handleError,
        () => {
          console.log('update success!');
        }
      );
    }
  }

  // go to personal page
  gotoPersionalPage(authorId: any) {
    this.router.navigate(['/user', authorId]);
    // window.location.href = '/user/' + authorId;
  }
}
