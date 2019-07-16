import { Component, OnInit, Input } from '@angular/core';
import { CompanionPost } from 'src/app/model/CompanionPost';

@Component({
  selector: 'app-companion-post-small',
  templateUrl: './companion-post-small.component.html',
  styleUrls: ['./companion-post-small.component.css']
})
export class CompanionPostSmallComponent implements OnInit {

  @Input() companionPost: CompanionPost;
  constructor() { }

  ngOnInit() {
  }

  getShortDescription(htmlContent) {
    // Convert html string to DOM object
    const div = document.createElement('div');
    div.innerHTML = htmlContent.trim();

    const pTags = div.getElementsByTagName('p');
    let pContent = '';
    for (let i = 0; i < pTags.length; i++) {
      pContent += pTags.item(i).innerText.trim() + ' ';

      if (pContent.length > 80) {
        break;
      }
    }

    if (pContent.length > 80) {
      pContent = pContent.substr(0, 80) + '...';
    }

    return pContent;
  }

  getDays() {
    const from = new Date(this.companionPost.from);
    const to = new Date(this.companionPost.to);
    const milisecs = to.getTime() - from.getTime();
    const days = Math.round(milisecs / (24 * 60 * 60 * 1000));
    return days;
  }

  gotoPersionalPage(authorId: any) {
    window.location.href = '/user/' + authorId;
  }

}
