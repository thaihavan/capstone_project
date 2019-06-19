import { Component, OnInit, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/core/services/user-service/user.service';
import { User } from 'src/app/model/User';

@Component({
  selector: 'app-personal-page',
  templateUrl: './personal-page.component.html',
  styleUrls: ['./personal-page.component.css']
})
export class PersonalPageComponent implements OnInit {
  user: User;
  gender = '';
  componentRefer: any;
  coverImage = '../../../assets/coverimg.jpg';
  avatar = '../../../assets/img_avatar.png';
  title = 'angular-material-tab-router';
  navLinks: any[];
  activeLinkIndex = 0;
  isScrollTopShow = false;
  topPosToStartShowing = 100;
  @HostListener('window:scroll') checkScroll() {
    const scrollPosition =
      window.pageYOffset ||
      document.documentElement.scrollTop ||
      document.body.scrollTop ||
      0;
    if (scrollPosition >= this.topPosToStartShowing) {
      this.isScrollTopShow = true;
    } else {
      this.isScrollTopShow = false;
    }
  }

  constructor(private router: Router, private userService: UserService) {
    this.navLinks = [
      {
        label: 'Bài viết',
        link: './articles',
        index: 0
      },
      {
        label: 'Chuyến đi',
        link: './virtual-trips',
        index: 1
      },
      {
        label: 'Tìm bạn đồng hành',
        link: './companion-posts',
        index: 2
      }
    ];
    this.user = new User();
    const account = JSON.parse(localStorage.getItem('Account'));
    console.log(account);
    this.getInforUser(account.userId);
  }
  ngOnInit(): void {
    this.router.events.subscribe(res => {
      this.activeLinkIndex = this.navLinks.indexOf(
        this.navLinks.find(tab => tab.link === '.' + this.router.url)
      );
    });
  }
  onActivate(componentRef) {
    this.componentRefer = componentRef;
  }
  onScroll() {
    this.componentRefer.onScroll();
  }
  gotoTop() {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
  }

  getInforUser(userId: string) {
    this.userService.getUserById(userId).subscribe((data: any) => {
      this.user.ContributionPoint = data.contributionPoint;
      this.user.Dob = data.dob;
      this.user.DisplayName = data.displayName;
      this.user.FirstName = data.firstName;
      this.user.Gender = data.gender;
      this.user.Interested = data.interested;
      this.user.LastName = data.lastName;
      this.user.UserName = data.userName;
      if (this.user.Gender === true) {
        this.gender = 'Nam';
      } else {
        this.gender = 'Nữ';
      }
      console.log(this.user);
    });
  }
}
