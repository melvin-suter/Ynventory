import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CardModel } from '../models/card.model';
import { CollectionModel } from '../models/collection.model';
import { CollectionItemModel } from '../models/collectionItem.model';
import { FolderModel } from '../models/folder.model';

@Injectable({
  providedIn: 'root'
})
export class CollectionService {

  constructor(private http: HttpClient) { }

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
    return this.http.get<CollectionModel[]>('/api/collections/' + collectionId + '/folders');
  }

  getCollectionItem(collectionId:number, itemId:number):Observable<CollectionItemModel>{
    return this.http.get<CollectionItemModel>('/api/collections/' + collectionId + '/folders/' + itemId);
  }

  createCollectionItem(collectionId:number, data:CollectionItemModel){
    return this.http.post<CollectionItemModel[]>('/api/collections/' + collectionId + '/folders', data);
  }

  deleteCollectionItem(collectionId:number, itemId:number){
    return this.http.delete<any>('/api/collections/' + collectionId + '/folders/' + itemId);
  }

  updateCollectionItem(collectionId:number, data:CollectionItemModel){
    return this.http.put<CollectionItemModel[]>('/api/collections/' + collectionId + '/folders/' + data.id, data);
  }

  /****************
   *    Cards
   ****************/

   getCollectionItemCards(collectionId:number, collectionItemId:number):Observable<CardModel[]>{
    return this.http.get<CollectionModel[]>('/api/collections/' + collectionId + '/folders/' + collectionItemId + '/cards');
   }

   getCollectionItemCard(collectionId:number, itemId:number):Observable<CardModel>{
     return this.http.get<CardModel>('/api/collections/' + collectionId + '/folders/' + itemId + '/cards');
   }

   deleteCollectionItemCard(collectionId:number, itemId:number, cardId:number){
     return this.http.delete<any>('/api/collections/' + collectionId + '/folders/' + itemId + '/cards/' + cardId);
   }
 
   updateCollectionItemCard(collectionId:number, itemId:number, data:CardModel){
    // ToDo FIX for missing fields:
    data.cardMetadataId = data.metadata?.id;
     return this.http.put<CardModel[]>('/api/collections/' + collectionId + '/folders/' + itemId + "/cards/" + data.id, data);
   }

   createCollectionItemCard(collectionId:number, itemId:number, data:CardModel){
     return this.http.post<any>('/api/collections/' + collectionId + '/folders/' + itemId + "/cards" , data);
   }




















  getCollectionFolders(id:number):FolderModel[]{
    let data:FolderModel[] = [
      { id: 1, name: "Folder Red", description: "", cardCount: 221},
      { id: 2, name: "Folder Green", description: "Super descriptive text", cardCount: 151},
      { id: 3, name: "Folder Blue", description: "Super descriptive text", cardCount: 512}
    ];
    return data;
  }


  getFolder(id:number):FolderModel{
    return { id: 1, name: "Folder Red", description: "", cardCount: 221};
  }

}
