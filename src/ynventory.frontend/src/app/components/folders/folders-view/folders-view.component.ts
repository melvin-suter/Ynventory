import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CardModel } from 'src/app/models/card.model';
import { FolderModel } from 'src/app/models/folder.model';
import { CardService } from 'src/app/services/card.service';
import { CollectionService } from 'src/app/services/collection.service';
import { ScryfallService } from 'src/app/services/scryfall.service';


@Component({
  selector: 'app-folders-view',
  templateUrl: './folders-view.component.html',
  styleUrls: ['./folders-view.component.scss']
})
export class FoldersViewComponent implements OnInit {

 
  folder?:FolderModel;
  

  constructor(private collectionService: CollectionService, private route: ActivatedRoute) { 
    this.route.params.subscribe(params => {
      this.folder = collectionService.getFolder(params['id']);
    });
  }

  ngOnInit(): void {
  }
  



}
