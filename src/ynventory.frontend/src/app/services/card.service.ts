import { Injectable } from '@angular/core';
import { CardModel } from '../models/card.model';

@Injectable({
  providedIn: 'root'
})
export class CardService {

  exampleData:CardModel[] =  [
    {id: 1, name: "Card 1", cardMetadataId: "6f40427d-231c-4baa-b1ed-4a2200dd5186", foil: 'Foil', quantity: 5},
    {id: 1, name: "Card 2", cardMetadataId: "2a46af75-3880-4141-b26e-19834d67e7a8", foil: 'Foil', quantity: 5},
    {id: 1, name: "Card 3", cardMetadataId: "db8705b7-e851-483e-a2af-bd309e62f079", foil: 'Foil', quantity: 3}
  ];

  constructor() { }

  public getAllCards():CardModel[]{
    return this.exampleData;
  }

  public getCards(folderID:number):CardModel[]{
    return this.exampleData;
  }

  public getCard(cardID:number):CardModel {
    return this.exampleData[0];
  }
}
