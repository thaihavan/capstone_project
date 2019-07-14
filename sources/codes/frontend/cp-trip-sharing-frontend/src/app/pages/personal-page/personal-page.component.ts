import { Component, OnInit, HostListener, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { User } from 'src/app/model/User';
import { MatDialog } from '@angular/material/dialog';
import { InitialUserInformationPageComponent } from '../initial-user-information-page/initial-user-information-page.component';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { ListFollowComponent } from 'src/app/shared/components/list-follow/list-follow.component';
import { MessagePopupComponent } from 'src/app/shared/components/message-popup/message-popup.component';
import { Title } from '@angular/platform-browser';
import { SendMessagePopupComponent } from 'src/app/shared/components/send-message-popup/send-message-popup.component';
import { Author } from 'src/app/model/Author';
import { UploadImageComponent } from 'src/app/shared/components/upload-image/upload-image.component';
import { ReportPopupComponent } from 'src/app/shared/components/report-popup/report-popup.component';

@Component({
  selector: 'app-personal-page',
  templateUrl: './personal-page.component.html',
  styleUrls: ['./personal-page.component.css']
})
export class PersonalPageComponent implements OnInit {
  @ViewChild('uploadImage') uploadImage: UploadImageComponent;
  user: User;
  usergetLocalStorage: any;
  gender = '';
  coverImage = '../../../assets/cover-image.png';
  avatar = '../../../assets/img_avatar.png';
  title = 'angular-material-tab-router';
  navLinks: any[];
  activeLinkIndex = 0;
  userId: string;
  listUser: User[] = [];
  isDisplayNav = true;
  follow = false;
  listUserIdFollowing: any[] = [];
  token: any;
  myProfile: Author;

  constructor(private router: Router, private userService: UserService, public dialog: MatDialog,
              private route: ActivatedRoute, private titleService: Title) {
    this.titleService.setTitle('Người dùng');
    this.navLinks = [
      {
        label: 'Bài viết',
        link: './bai-viet',
        index: 0
      },
      {
        label: 'Chuyến đi',
        link: './chuyen-di',
        index: 1
      },
      {
        label: 'Tìm bạn đồng hành',
        link: './tim-ban-dong-hanh',
        index: 2
      }
    ];
    this.user = new User();
    this.userId = this.route.snapshot.paramMap.get('userId');
    this.usergetLocalStorage = JSON.parse(localStorage.getItem('User'));
    if (this.userId != null) {
      this.getInforUser(this.userId);
    } else {
      this.userId = this.usergetLocalStorage.id;
      this.getInforUser(this.userId);
    }
    this.token = localStorage.getItem('Token');
    this.getStates();
  }
  ngOnInit(): void {
    this.myProfile = new Author();
    if (this.router.url.indexOf('da-danh-dau') !== -1) {
      this.isDisplayNav = false;
    }

    if (this.router.url.indexOf('danh-sach-chan') !== -1) {
      this.isDisplayNav = false;
    }

    this.router.events.subscribe(res => {
      this.activeLinkIndex = this.navLinks.indexOf(
        this.navLinks.find(tab => tab.link === '.' + this.router.url)
      );
    });
  }

  getStates(): void {
    this.listUserIdFollowing = JSON.parse(localStorage.getItem('listUserIdFollowing'));
    if (this.listUserIdFollowing != null) {
      this.follow = this.listUserIdFollowing.indexOf(this.userId) !== -1;
    }
  }

  getInforUser(userId: string) {
    this.userService.getUserById(userId).subscribe((data: any) => {
      console.log(data);
      this.user.UserId = data.id;
      this.user.ContributionPoint = data.contributionPoint;
      this.user.DisplayName = data.displayName;
      this.user.FirstName = data.firstName;
      this.user.Gender = data.gender;
      this.user.Dob = data.dob;
      if (this.user.Gender === true) {
        this.gender = 'Nam';
      } else {
        this.gender = 'Nữ';
      }
      this.user.Interested = data.interested;
      this.user.LastName = data.lastName;
      this.user.UserName = data.userName;
      this.user.Address = data.address;
      this.user.FollowerCount = data.followerCount;
      this.user.FollowingCount = data.followingCount;
      this.user.Avatar = data.avatar;
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

  callUpdateProfile() {
    this.openDialog();
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(InitialUserInformationPageComponent, {
      width: '60%',
      data: {
        toppics: [],
        destinations: [],
      }
    });
    const instance = dialogRef.componentInstance;
    instance.user.UserId = this.userId;
    instance.user.UserName = this.user.UserName;
    instance.user.DisplayName = this.user.DisplayName;
    instance.user.FirstName = this.user.FirstName;
    instance.user.Gender = this.user.Gender;
    instance.user.Interested = this.user.Interested;
    instance.user.LastName = this.user.LastName;
    instance.user.UserName = this.user.UserName;
    instance.user.Address = this.user.Address;
    instance.user.Dob = this.user.Dob;
    instance.user.Gender = this.user.Gender;
  }

  showFollowingUser() {
    this.userService.getAllFollowing(this.userId).subscribe((result: any) => {
      this.listUser = result;
      this.openDialogFollow(this.user.FollowingCount + ' người bạn đang theo dõi', this.listUser);
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

  showFolowerUser() {
    this.userService.getAllFollower(this.userId).subscribe((result: any) => {
      this.listUser = result;
      this.openDialogFollow(this.user.FollowerCount + ' người đang theo dõi bạn', this.listUser);
    }, (err: HttpErrorResponse) => {
      console.log(err);
    });
  }

  openDialogFollow(title: any, listUsers: any) {
    const dialogRef = this.dialog.open(ListFollowComponent, {
      height: '450px',
      width: '50%'
    });
    const instance = dialogRef.componentInstance;
    instance.listUser = listUsers;
    instance.title = title;
  }

  blockUserById(userId: any) {
    const token = localStorage.getItem('Token');
    if (token != null) {
      this.userService.addBlock(userId, token).subscribe((result: any) => {
        this.openDialogMessageConfirm();
      });
    }
  }

  openDialogMessageConfirm() {
    const user = JSON.parse(localStorage.getItem('User'));
    const dialogRef = this.dialog.open(MessagePopupComponent, {
      width: '320px',
      height: 'auto',
      position: {
        top: '20px'
      },
      disableClose: true
    });
    const instance = dialogRef.componentInstance;
    instance.message.messageText = 'Chặn người dùng thành công!';
    instance.message.url = '/user/' + user.id + '/danh-sach-chan';
  }

  openSendMessagePopup() {
    const dialogRef = this.dialog.open(SendMessagePopupComponent, {
      width: '40%',
      height: 'auto',
      position: {
        top: '100px'
      },
      disableClose: false
    });
    const instance = dialogRef.componentInstance;
    instance.receiver = this.user;
  }

  followPerson(userId: any) {
    if (this.follow === false) {
      this.userService.addFollow(userId, this.token).subscribe((data: any) => {
        this.follow = true;
        this.listUserIdFollowing.push(userId);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    } else {
      this.userService.unFollow(userId, this.token).subscribe((data: any) => {
        this.follow = false;
        const unfollow = this.listUserIdFollowing.indexOf(userId);
        this.listUserIdFollowing.splice(unfollow, 1);
        localStorage.setItem('listUserIdFollowing', JSON.stringify(this.listUserIdFollowing));
      }, (err: HttpErrorResponse) => {
        console.log(err);
      });
    }
  }
  // change avatar image
  changeAvatar() {
    this.uploadImage.file.nativeElement.click();
  }
  // Image crop upload avatar
  ImageCropted(image) {
    this.user.Avatar = image;
    const user = JSON.parse(localStorage.getItem('User'));
    user.avatar = image;
    this.userService.updateUser(user).subscribe(
      res => {

      },
      (err) => {
        console.log('update avatar image error', err.message);
      },
      () => {
        localStorage.setItem('User', JSON.stringify(user));
      }
    );
  }

  reportUser(userId: any) {
    this.openDialogRepoetUser('Báo cáo người dùng');
  }

  openDialogRepoetUser(title: string) {
    const dialogRef = this.dialog.open(ReportPopupComponent, {
      width: '400px',
      height: 'auto',
      position: {
        top: '10px'
      },
      disableClose: false
    });
    const instance = dialogRef.componentInstance;
    instance.title = title;
  }
}
