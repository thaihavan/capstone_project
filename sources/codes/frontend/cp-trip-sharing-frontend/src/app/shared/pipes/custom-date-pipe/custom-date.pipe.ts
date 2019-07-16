import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'customDate'
})
export class CustomDatePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    const date = new Date(value);
    const now = new Date();

    const milisecs = now.getTime() - date.getTime();
    const secs = Math.floor(milisecs / 1000);
    const mins = Math.floor(secs / 60);
    const hours = Math.floor(mins / 60);
    const days = Math.floor(hours / 24);

    let result = `${date.getDate()}/${date.getMonth()}/${date.getFullYear()}`;

    if (secs < 60) {
      result = `${secs} giây trước`;
    } else if (mins < 60) {
      result = `${mins} phút trước`;
    } else if (hours < 24) {
      result = `${hours} giờ trước`;
    } else if (days < 7) {
      result = `${days} ngày trước`;
    }

    return result;
  }

}
