import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cardColorReplace'
})
export class CardColorReplacePipe implements PipeTransform {

  transform(value?: string): string {
    return String(value).replace(/\{([A-Z0-9]+)\}/g,"<img class=\"card-symbol-icon\" src=\"https://svgs.scryfall.io/card-symbols/$1.svg\">");
  }

}
