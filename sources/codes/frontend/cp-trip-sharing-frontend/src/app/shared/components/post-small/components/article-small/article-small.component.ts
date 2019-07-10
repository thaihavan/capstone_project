import { Component, OnInit, Input } from '@angular/core';
import { Article } from 'src/app/model/Article';

@Component({
  selector: 'app-article-small',
  templateUrl: './article-small.component.html',
  styleUrls: ['./article-small.component.css']
})
export class ArticleSmallComponent implements OnInit {

  @Input() article: Article;
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

  gotoPersionalPage(authorId: any) {
    window.location.href = '/user/' + authorId;
  }

}
