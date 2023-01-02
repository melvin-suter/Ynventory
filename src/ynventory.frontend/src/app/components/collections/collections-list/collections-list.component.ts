import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, switchMap } from 'rxjs';
import { CollectionModel } from 'src/app/models/collection.model';
import { CollectionService } from 'src/app/services/collection.service';


@Component({
  selector: 'app-collections-list',
  templateUrl: './collections-list.component.html',
  styleUrls: ['./collections-list.component.scss']
})
export class CollectionsListComponent {

  // Observable Handler Shit
  collectionsObservable$:Observable<CollectionModel[]>;
  refresh$ = new BehaviorSubject<number>(1);

  // Table Data
  collections:CollectionModel[] = [];
  selectedCollections:CollectionModel[] = [];

  //Modal Data
  showAddModal:boolean = false;
  showEditModal:boolean = false;
  showDeleteModal:boolean = false;
  modalData:CollectionModel = {};
  modalDataDelete:CollectionModel[] = [];


  constructor(private collectionService:CollectionService) { 
    this.collectionsObservable$ = this.refresh$.pipe(switchMap((_:any) => {
      return this.collectionService.getCollections();
    }));

    this.collectionsObservable$.subscribe( (data:CollectionModel[]) => {
      this.selectedCollections = [];
      this.collections = data;
    });
  }

  openAddModal() {
    this.modalData = {};
    this.showAddModal = true;
  }

  openEditModal(){
    this.modalData = JSON.parse(JSON.stringify(this.selectedCollections[0]));
    this.showEditModal = true;
  }

  openDeleteModal(){
    this.modalDataDelete = this.selectedCollections;
    this.showDeleteModal = true;
  }

  createItem(){
    this.collectionService.createCollection(this.modalData).subscribe(() => {
      this.refresh$.next(2);
      this.showAddModal = false;
      this.modalData = {};
    });
  }

  saveItem(){
    this.collectionService.updateCollection(this.modalData).subscribe(() => {
      this.refresh$.next(2);
      this.showEditModal = false;
      this.modalData = {};
    });
  }

  deleteItem(){
    this.modalDataDelete.forEach( (item:CollectionModel) => {

      this.collectionService.deleteCollection(item.id!).subscribe(() => {
        this.refresh$.next(2);
      });

    });

    this.modalDataDelete = [];
    this.showDeleteModal = false;
  }

}
