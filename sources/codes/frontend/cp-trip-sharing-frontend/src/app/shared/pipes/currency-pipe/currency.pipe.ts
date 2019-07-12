import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'currency'
})
export class CurrencyPipe implements PipeTransform {
  transform(value: any, args?: any): any {
    value = value.toString();
    let convertStr = '';
    // const strLenght = value.length;
    // const numDot = Math.floor(strLenght / 3);
    // for (let index = 1; index <= numDot; index++) {
    //   let element;
    //   if (strLenght !== index * 3) {
    //     element =
    //       '.' + value.slice(strLenght - 3 * index, strLenght - 3 * (index - 1));
    //     convertStr = element + convertStr;
    //   }
    //   if (index === numDot) {
    //     element = value.slice(0, strLenght - numDot * 3);
    //     convertStr = element + convertStr;
    //   }
    // }
    let strLength = 0;
    for (let index = value.length - 1 ; index >= 0; index--) {
      strLength += 1;
      convertStr = value.charAt(index) + convertStr;
      if (strLength % 3 === 0 && index !== value.length && index !== 0) {
      convertStr = '.' + convertStr;
      }
    }
    return convertStr;
  }
}
