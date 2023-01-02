import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CardModel } from '../models/card.model';
import { DeckModel } from '../models/deck.model';

@Injectable({
  providedIn: 'root'
})
export class DeckService {

  constructor(private http: HttpClient) { }


  /****************
   *  Decks
   ****************/

   getDecks():Observable<DeckModel[]>{
    return this.http.get<DeckModel[]>('/api/decks');
  }

  getDeck(id:number):Observable<DeckModel>{
    return this.http.get<DeckModel>('/api/decks/' + id);
  }

  createDeck(data:DeckModel){
    return this.http.post<DeckModel[]>('/api/decks', data);
  }

  deleteDeck(id:number){
    return this.http.delete<any>('/api/decks/' + id);
  }

  updateDeck(data:DeckModel){
    return this.http.put<DeckModel[]>('/api/decks/' + data.id, data);
  }


  /****************
   *    Cards
   ****************/

  /****************
   *    Cards
   ****************/

  getDeckCards(deckId:number):Observable<CardModel[]>{
    return this.http.get<CardModel[]>('/api/decks/' + deckId + '/cards');
  }

  getDeckCard(deckId:number):Observable<CardModel>{
    return this.http.get<CardModel>('/api/decks/' + deckId + '/cards');
  }

  deleteDeckCard(deckId:number, cardId:number){
    return this.http.delete<any>('/api/decks/' + deckId + '/cards/' + cardId);
  }

  updateDeckCard(deckId:number, data:CardModel){
    data.cardMetadataId = data.metadata?.id;

    return this.http.put<CardModel[]>('/api/decks/' + deckId + "/cards/" + data.id, data);
  }

  createDeckCard(deckId:number, data:CardModel){
    return this.http.post<any>('/api/decks/' + deckId + "/cards" , data);
  }
}
