import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Observable, switchMap } from 'rxjs';
import { CollectionModel } from 'src/app/models/collection.model';
import { CollectionItemModel } from 'src/app/models/collectionItem.model';
import { FolderModel } from 'src/app/models/folder.model';
import { CollectionService } from 'src/app/services/collection.service';


@Component({
  selector: 'app-collection-folders',
  templateUrl: './collection-folders.component.html',
  styleUrls: ['./collection-folders.component.scss']
})
export class CollectionFoldersComponent  {

  // Observable Handler Shit
  collectionItemsObservable$?:Observable<CollectionItemModel[]>;
  refresh$ = new BehaviorSubject<number>(1);

  // Table Data
  collection?:CollectionModel;
  collectionItems:CollectionItemModel[] = [];
  selectedCollectionItems:FolderModel[] = [];
  collectionId:number = -1;

  // Modal Data
  showAddModal:boolean = false;
  showEditModal:boolean = false;
  showDeleteModal:boolean = false;
  modalData:CollectionItemModel = {};
  modalDataDelete:CollectionItemModel[] = [];


  
  constructor(private collectionService:CollectionService,private route: ActivatedRoute) { 

    this.route.params.subscribe(params => {

      this.collectionId = params['colid'];

      collectionService.getCollection(params['colid']).subscribe( (data:CollectionModel) => {
        this.collection = data;
      });

      this.collectionItemsObservable$ = this.refresh$.pipe(switchMap((_:any) => {
        return this.collectionService.getCollectionItems(params['colid']);
      }));

      this.collectionItemsObservable$.subscribe( (data:CollectionItemModel[]) => {
        this.selectedCollectionItems = [];
        this.collectionItems = data;
      });
      

    });

  }

  openAddModal() {
    this.modalData = {};
    this.showAddModal = true;
  }

  openEditModal(){
    this.modalData = JSON.parse(JSON.stringify(this.selectedCollectionItems[0]));
    this.showEditModal = true;
  }

  openDeleteModal(){
    this.modalDataDelete = this.selectedCollectionItems;
    this.showDeleteModal = true;
  }

  createItem(){
    this.collectionService.createCollectionItem(this.collectionId,this.modalData).subscribe(() => {
      this.refresh$.next(2);
      this.showAddModal = false;
      this.modalData = {};
    });
  }

  saveItem(){
    this.collectionService.updateCollectionItem(this.collectionId,this.modalData).subscribe(() => {
      this.refresh$.next(2);
      this.showEditModal = false;
      this.modalData = {};
    });
  }

  deleteItem(){
    this.modalDataDelete.forEach( (item:CollectionModel) => {

      this.collectionService.deleteCollectionItem(this.collectionId,item.id!).subscribe(() => {
        this.refresh$.next(2);
      });

    });

    this.modalDataDelete = [];
    this.showDeleteModal = false;
  }


}
