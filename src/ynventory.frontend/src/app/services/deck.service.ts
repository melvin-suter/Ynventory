import { Injectable } from '@angular/core';
import { CardModel } from '../models/card.model';
import { DeckModel } from '../models/deck.model';

@Injectable({
  providedIn: 'root'
})
export class DeckService {

  constructor() { }


  getDecks():DeckModel[]{
    let data:DeckModel[] = [
      { id: 1, name: "Super duper deck", description: "", cardCount: 69}
    ];
    return data;
  }

  getDeck(deckID:number):DeckModel{
    return { id: 1, name: "Super duper deck", description: "", cardCount: 69};
  }


  public getCards(deckID:number):CardModel[]{
    return [
      {id: 1, name: "Card 1", scryfallID: "scry1", foil:'nonfoil' , quantity: 5},
      {id: 1, name: "Card 2", scryfallID: "scry1", foil:'nonfoil' , quantity: 5},
      {id: 1, name: "Card 3", scryfallID: "scry1", foil:'nonfoil' , quantity: 3}
    ]
  }

}
