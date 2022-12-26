import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CollectionModel } from 'src/app/models/collection.model';
import { FolderModel } from 'src/app/models/folder.model';
import { CollectionService } from 'src/app/services/collection.service';

@Component({
  selector: 'app-collections-folders',
  templateUrl: './collections-folders.component.html',
  styleUrls: ['./collections-folders.component.scss']
})
export class CollectionsFoldersComponent implements OnInit {

 
  collection?:CollectionModel;
  folders:FolderModel[] = [];
  selectedFolders:FolderModel[] = [];

  constructor(private collectionService:CollectionService,private route: ActivatedRoute) { 
    this.route.params.subscribe(params => {
      this.collection = collectionService.getCollection(params['colid']);
      this.folders = collectionService.getCollectionFolders(params["colid"]);
    });

  }

  ngOnInit(): void {
  }

}
