import { Injectable } from '@angular/core';
import { CardModel } from '../models/card.model';

@Injectable({
  providedIn: 'root'
})
export class CardService {

  constructor() { }

  public getCards(folderID:number):CardModel[]{
    return [
      {id: 1, name: "Card 1", scryfallID: "scry1", quantity: 5},
      {id: 1, name: "Card 2", scryfallID: "scry1", quantity: 5},
      {id: 1, name: "Card 3", scryfallID: "scry1", quantity: 3}
    ]
  }
}
