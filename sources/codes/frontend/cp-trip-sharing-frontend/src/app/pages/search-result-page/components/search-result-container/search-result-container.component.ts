import { Component, OnInit, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { PostFilter } from 'src/app/model/PostFilter';

@Component({
  selector: 'app-search-result-container',
  templateUrl: './search-result-container.component.html',
  styleUrls: ['./search-result-container.component.css']
})
export class SearchResultContainerComponent implements OnInit {

  tab: string;
  VALID_TABS = [
    'moi-nguoi',
    'bai-viet',
    'chuyen-di',
    'tim-ban-dong-hanh'
  ];

  showTimePeriod = true;
  showTopic = true;

  isLoading = false;
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

  constructor(private titleService: Title,
              private route: ActivatedRoute) {
    this.tab = this.route.snapshot.paramMap.get('tab');
    if (this.VALID_TABS.indexOf(this.tab) === -1) {
      this.tab = 'moi-nguoi';
    }
  }

  ngOnInit() {
  }

  getPosts(postFilter: PostFilter) {
    switch (this.tab) {
      case '':
        break;
      case 'moi-nguoi':
        break;
      case 'bai-viet':
        break;
      case 'chuyen-di':
        break;
      case 'tim-ban-dong-hanh':
        break;
    }
  }

  submitFilter(postFilter: PostFilter) {
    console.log(postFilter);
    this.getPosts(postFilter);
  }

  onScroll() {
    this.isLoading = true;

    // Continue loading data
    // this.getPost();
  }

  gotoTop() {
    window.scroll({
      top: 0,
      left: 0,
      behavior: 'smooth'
    });
  }

}
