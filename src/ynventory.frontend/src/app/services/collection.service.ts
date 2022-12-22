import { Injectable } from '@angular/core';
import { CollectionModel } from '../models/collection.model';
import { FolderModel } from '../models/folder.model';

@Injectable({
  providedIn: 'root'
})
export class CollectionService {

  constructor() { }

  getCollections():CollectionModel[]{
    let data:CollectionModel[] = [
      { id: 1, name: "Default Collection 1", description: "", cardCount: 1015},
      { id: 2, name: "Some Collection", description: "Super descriptive text", cardCount: 1512}
    ];
    return data;
  }

  getCollection(id:number):CollectionModel{
      return { id: 1, name: "Default Collection 1", description: "", cardCount: 1015};
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
