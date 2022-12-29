import { Component, OnInit } from '@angular/core';
import { CollectionModel } from 'src/app/models/collection.model';
import { CollectionService } from 'src/app/services/collection.service';


@Component({
  selector: 'app-collections-list',
  templateUrl: './collections-list.component.html',
  styleUrls: ['./collections-list.component.scss']
})
export class CollectionsListComponent implements OnInit {


  collections:CollectionModel[];
  selectedCollections:CollectionModel[] = [];

  showAddModal:boolean = false;
  showEditModal:boolean = false;
  showDeleteModal:boolean = false;

  modalData:CollectionModel = new CollectionModel();

  constructor(private collectionService:CollectionService) { 
    this.collections = collectionService.getCollections();
  }


  ngOnInit(): void {
  }

  createItem(){
    this.showAddModal = false;
  }

  openEditModal(){
    this.modalData = this.selectedCollections[0];
    this.showEditModal = true;
  }

  saveItem(){
    this.showEditModal = false;
  }

  deleteItem(){
    this.showDeleteModal = false;
  }

}
