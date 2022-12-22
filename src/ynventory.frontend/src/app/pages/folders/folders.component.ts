import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CollectionModel } from 'src/app/models/collection.model';
import { FolderModel } from 'src/app/models/folder.model';
import { CollectionService } from 'src/app/services/collection.service';

@Component({
  selector: 'app-folders',
  templateUrl: './folders.component.html',
  styleUrls: ['./folders.component.scss']
})
export class FoldersComponent implements OnInit {

  collection?:CollectionModel;
  folders?:FolderModel[];

  constructor(private collectionService:CollectionService,private route: ActivatedRoute) { 
    this.route.params.subscribe(params => {
      this.collection = collectionService.getCollection(params['id']);
      this.folders = collectionService.getCollectionFolders(params["id"]);
    });

  }

  ngOnInit(): void {

  }

}
