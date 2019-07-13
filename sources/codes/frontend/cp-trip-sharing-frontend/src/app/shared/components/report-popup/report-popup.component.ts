import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-report-popup',
  templateUrl: './report-popup.component.html',
  styleUrls: ['./report-popup.component.css']
})
export class ReportPopupComponent implements OnInit {
  title: string;
  showReasonOthers = false;
  listReasonString: string[] = ['Ảnh khỏa thân', 'Bạo lực', 'Quấy rối', 'Tự tử/Tự gây thương tích',
    'Spam', 'Bán hàng trái phep', 'Ngôn từ gây thù ghét', 'Khủng bố', 'Khác...'];
  listReasonSelect: string[] = [];
  checkActiveReasons = false;

  constructor() { }

  ngOnInit() {
  }

  getReason(reason: string) {
    if (reason === 'Khác...') {
      this.showReasonOthers = !this.showReasonOthers;
    }

    if (this.listReasonSelect.indexOf(reason) === -1) {
      this.listReasonSelect.push(reason);
    } else {
      const unselected = this.listReasonSelect.indexOf(reason);
      this.listReasonSelect.splice(unselected, 1);
    }
  }

  checkActiveReason(reason: string): boolean {
    if (this.listReasonSelect.indexOf(reason) !== -1) {
      return true;
    }
    return false;
  }
}
