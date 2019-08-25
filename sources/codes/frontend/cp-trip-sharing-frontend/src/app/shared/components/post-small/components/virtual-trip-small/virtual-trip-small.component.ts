import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { VirtualTrip } from 'src/app/model/VirtualTrip';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { IImage } from 'ng-simple-slideshow';

@Component({
  selector: 'app-virtual-trip-small',
  templateUrl: './virtual-trip-small.component.html',
  styleUrls: ['./virtual-trip-small.component.css'],
  animations: [
    trigger('myTrigger', [
      state('void', style({
        opacity: 0,
        left: '-100%',

      })),
      state('*', style({
        opacity: 1,
        left: '0%',
      })),
      transition('void => *', [animate('0.5s ease-in')]),
    ])
  ]
})
export class VirtualTripSmallComponent implements OnInit {

  @Input() virtualTrip: VirtualTrip;

  @ViewChild('slideShow') slideShow: any;
  imageSources: (string | IImage)[] = [];
  desTitle = '';

  constructor() { }

  ngOnInit() {
    this.setVirtualTripDisplay();
  }

  // tslint:disable-next-line: use-life-cycle-interface
  ngAfterViewInit(): void {
    // set slideshow indexchage
    this.slideShow.onIndexChanged.subscribe(slide => {
      if (this.slideShow.slideIndex === 0) {
        this.desTitle = '';
      } else {
        this.desTitle = this.virtualTrip.items[this.slideShow.slideIndex - 1].name;
      }
    });
  }

  // if article is virtualtrip get imageurl for slideshow.
  setVirtualTripDisplay() {
    if (this.virtualTrip.post.coverImage !== null && this.virtualTrip.post.coverImage !== '') {
      const url = {
        url: this.virtualTrip.post.coverImage,
        caption: '',
        href: '#'
      };
      this.imageSources.push(url);
    } else {
      // tslint:disable-next-line:max-line-length
      const url = {
        url: 'https://montehorizonte.com/wp-content/themes/fortunato-pro/images/no-image-box.png',
        caption: '',
        href: '#'
      };
      this.imageSources.push(url);
    }
    this.virtualTrip.items.forEach(art => {
      const url = {
        url: art.image,
        caption: art.name,
        href: '#'
      };
      this.imageSources.push(url);
    });
  }

  getShortDescription(textContent: string) {
    if (!textContent) {
      return '';
    }

    let shortDes = textContent.trim();
    if (textContent.length > 80) {
      shortDes = shortDes.substr(0, 80) + '...';
    }

    return shortDes;
  }

}
