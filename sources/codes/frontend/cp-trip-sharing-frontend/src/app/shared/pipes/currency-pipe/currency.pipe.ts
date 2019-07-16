import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'currency'
})
export class CurrencyPipe implements PipeTransform {
  transform(value: any, args?: any): any {
    value = value.toString();
    let convertStr = '';
    let strLength = 0;
    for (let index = value.length - 1 ; index >= 0; index--) {
      strLength += 1;
      convertStr = value.charAt(index) + convertStr;
      if (strLength % 3 === 0 && index !== value.length && index !== 0) {
      convertStr = ',' + convertStr;
      }
    }
    return convertStr;
  }
}
