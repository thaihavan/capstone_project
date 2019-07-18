import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'strCurrency'
})
export class StrCurrencyPipe implements PipeTransform {

  transform(value: any, args?: any): any {
    if (value !== undefined) {
      if (value !== null) {
        let text: string =  value.toString();
        text = text.replace(/[^\d,]/g, '0');
        const arrText = text.split(',');
        text = '';
        arrText.forEach(element => {
          text += element;
        });
        let convertStr = '';
        let strLength = 0;
        for (let index = text.length - 1 ; index >= 0; index--) {
          strLength += 1;
          convertStr = text.charAt(index) + convertStr;
          if (strLength % 3 === 0 && index !== text.length && index !== 0) {
          convertStr = ',' + convertStr;
          }
        }
        return convertStr;

      }
    }
    return '';
  }
}
