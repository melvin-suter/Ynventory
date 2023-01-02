import { Component, OnInit } from '@angular/core';
import { CollectionModel } from 'src/app/models/collection.model';
import { CollectionItemModel } from 'src/app/models/collectionItem.model';
import { ImportTaskModel } from 'src/app/models/import-task.model';
import { CollectionService } from 'src/app/services/collection.service';
import { ImportService } from 'src/app/services/import.service';

@Component({
  selector: 'app-tasks-list',
  templateUrl: './tasks-list.component.html',
  styleUrls: ['./tasks-list.component.scss']
})
export class TasksListComponent implements OnInit {

  tasks:ImportTaskModel[] = [];

  collections:CollectionModel[] = [];
  collectionItems:CollectionItemModel[] = [];
  selectedCollection?:CollectionModel;
  selectedCollectionItem?:CollectionItemModel;

  showAddModal:boolean = false;
  showErrorModal:boolean = false;
  errorModalTask?:ImportTaskModel;

  file?:File;
  fileName:string = "";
  newTask:ImportTaskModel = {
    collectionId: -1,
    collectionItemId: -1,
  };


  constructor(private importService:ImportService, private collectionService:CollectionService) {
    this.loadData();
  }

  collectionSelected(){
    this.collectionItems = [];
    this.collectionService.getCollectionItems(this.selectedCollection!.id!).subscribe( (data:CollectionItemModel[]) => {
      this.collectionItems = data;
    });
  }

  openErrorModal(task:ImportTaskModel){
    this.errorModalTask = task;
    this.showErrorModal = true;
  }

  loadData(){
    this.importService.getTasks().subscribe( (data:ImportTaskModel[]) => {
      this.tasks = data;
    });
    this.collectionService.getCollections().subscribe( (data:CollectionModel[]) => {
      this.collections = data;
      this.selectedCollection = data[0];
      this.collectionSelected();
    });
  }

  ngOnInit(): void {
  }

  openAddModal() {
    this.showAddModal = true;
  }

  createTask(){
    this.newTask.collectionId = this.selectedCollection!.id!;
    this.newTask.collectionItemId = this.selectedCollectionItem!.id!;
    this.importService.createTasks(this.newTask).subscribe( () => {
      this.loadData();
      this.showAddModal = false;
    });
  }

  fileChanged(event:any){
    let fileReader = new FileReader();
    fileReader.onload = (e) => {
      this.newTask.fileData = String(fileReader.result);
    }
    fileReader.readAsText(event.target.files[0]);
    
    this.newTask.fileName = event.target.files[0].name;
  }

}
