import { Component, OnInit, HostListener } from '@angular/core';
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

@Component({
  selector: 'app-personal-page',
  templateUrl: './personal-page.component.html',
  styleUrls: ['./personal-page.component.css']
})
export class PersonalPageComponent implements OnInit {
  user: User;
  gender = '';
  coverImage = '../../../assets/coverimg.jpg';
  avatar = '../../../assets/img_avatar.png';
  title = 'angular-material-tab-router';
  navLinks: any[];
  activeLinkIndex = 0;
  userId: string;
  listUser: User[] = [];
  showDOB = false;
  showGender = false;
  showAddress = false;
  isDisplayNav = true;

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
    if (this.userId != null) {
      this.getInforUser(this.userId);
    } else {
      const user = JSON.parse(localStorage.getItem('User'));
      this.userId = user.id;
      this.getInforUser(this.userId);
    }

  }
  ngOnInit(): void {
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

  getInforUser(userId: string) {
    this.userService.getUserById(userId).subscribe((data: any) => {
      console.log(data);
      this.user.UserId = data.id;
      this.user.ContributionPoint = data.contributionPoint;
      if (data.dob != null) {
        this.user.Dob = data.dob;
      } else {
        this.showDOB = true;
      }
      this.user.DisplayName = data.displayName;
      this.user.FirstName = data.firstName;
      if (data.gender != null) {
        this.user.Gender = data.gender;
        if (this.user.Gender === true) {
          this.gender = 'Nam';
        } else {
          this.gender = 'Nữ';
        }
      } else {
        this.showGender = true;
      }
      this.user.Interested = data.interested;
      this.user.LastName = data.lastName;
      this.user.UserName = data.userName;
      if (data.address !== '') {
        this.user.Address = data.address;
      } else {
        this.showAddress = true;
      }
      this.user.FollowerCount = data.followerCount;
      this.user.FollowingCount = data.followingCount;
      console.log(this.user);
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
      width: '380px',
      height: '200px',
      position: {
        top: '10px'
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
}
