import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';
import { catchError, map, mergeAll, Observable, of } from 'rxjs';
import { CardModel } from '../models/card.model';
import { CollectionModel } from '../models/collection.model';
import { CollectionItemModel } from '../models/collectionItem.model';

@Injectable({
  providedIn: 'root'
})
export class CollectionService {

  constructor(private http: HttpClient, private messageService:MessageService) { }

  /****************
   *  Collections
   ****************/

  getCollections():Observable<CollectionModel[]>{
    return this.http.get<CollectionModel[]>('/api/collections');
  }

  getCollection(id:number):Observable<CollectionModel>{
    return this.http.get<CollectionModel>('/api/collections/' + id);
  }

  createCollection(data:CollectionModel){
    return this.http.post<CollectionModel[]>('/api/collections', data);
  }

  deleteCollection(id:number){
    return this.http.delete<any>('/api/collections/' + id);
  }

  updateCollection(data:CollectionModel){
    return this.http.put<CollectionModel[]>('/api/collections/' + data.id, data);
  }

  /****************
   * CollectionItems
   ****************/

  getCollectionItems(collectionId:number):Observable<CollectionItemModel[]>{
    return this.http.get<CollectionModel[]>('/api/collections/' + collectionId + '/items');
  }

  getCollectionItem(collectionId:number, itemId:number):Observable<CollectionItemModel>{
    return this.http.get<CollectionItemModel>('/api/collections/' + collectionId + '/items/' + itemId);
  }

  createCollectionItem(collectionId:number, data:CollectionItemModel){
    return this.http.post<CollectionItemModel[]>('/api/collections/' + collectionId + '/items', data);
  }

  deleteCollectionItem(collectionId:number, itemId:number){
    return this.http.delete<any>('/api/collections/' + collectionId + '/items/' + itemId);
  }

  updateCollectionItem(collectionId:number, data:CollectionItemModel){
    return this.http.put<CollectionItemModel[]>('/api/collections/' + collectionId + '/items/' + data.id, data);
  }

  /****************
   *    Cards
   ****************/

  getCollectionItemCards(collectionId:number, collectionItemId:number):Observable<CardModel[]>{
    return this.http.get<CollectionModel[]>('/api/collections/' + collectionId + '/items/' + collectionItemId + '/cards');
  }

  getCollectionItemCard(collectionId:number, itemId:number):Observable<CardModel>{
    return this.http.get<CardModel>('/api/collections/' + collectionId + '/items/' + itemId + '/cards');
  }

  deleteCollectionItemCard(collectionId:number, itemId:number, cardId:number){
    return this.http.delete<any>('/api/collections/' + collectionId + '/items/' + itemId + '/cards/' + cardId);
  }

  updateCollectionItemCard(collectionId:number, itemId:number, data:CardModel){
  // Add metadata id
  data.cardMetadataId = data.metadata?.id;

    return this.http.put<CardModel[]>('/api/collections/' + collectionId + '/items/' + itemId + "/cards/" + data.id, data);
  }

  createCollectionItemCard(collectionId:number, itemId:number, data:CardModel){
    return this.http.post<any>('/api/collections/' + collectionId + '/items/' + itemId + "/cards" , data);
  }

  moveCollectionItemCard(cardId:number, from:{collectionId:number, collectionItemId:number}, to:{collectionId:number, collectionItemId:number}, quantity:number):Observable<any>{
    return this.http.post<any>('/api/collections/' + from.collectionId + '/items/' + from.collectionItemId + '/cards/' + cardId + '/move', {
      targetCollectionId: to.collectionId,
      targetCollectionItemId: to.collectionItemId,
      quantity: quantity
    });
  
  }



}
