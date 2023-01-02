import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';
import { catchError, map, mergeAll, Observable, of } from 'rxjs';
import { CardModel } from '../models/card.model';
import { CollectionModel } from '../models/collection.model';
import { CollectionItemModel } from '../models/collectionItem.model';
import { FolderModel } from '../models/folder.model';

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

  moveCollectionItemCard(card:CardModel, from:{collectionId:number, collectionItemId:number}, to:{collectionId:number, collectionItemId:number}, doneAction:Function){
  // Add metadata id
  card.cardMetadataId = card.metadata?.id;

  // Add Card on new Collection Item
  this.http.post<any>('/api/collections/' + to.collectionId + '/items/' + to.collectionItemId + "/cards" , card).pipe(
    map((v) => v),
    catchError(err => {
      this.messageService.add({severity: 'danger', summary: 'Move Failed', detail: 'there was an error during card creation: ' + err});

      doneAction();
      return of();
    }
  )).subscribe( () => {
      this.http.delete<any>('/api/collections/' + from.collectionId + '/items/' + from.collectionItemId + '/cards/' + card.id).pipe(
        map((v) => v),
        catchError(err => {
          this.messageService.add({severity: 'danger', summary: 'Move Failed', detail: 'there was an error during card deletion: ' + err});
  
          doneAction();
          return of();
        }
      )).subscribe( () => {
        doneAction();
      });
  });
  }



}
