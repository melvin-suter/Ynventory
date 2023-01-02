import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';
import { CardModel } from 'src/app/models/card.model';
import { CollectionItemModel } from 'src/app/models/collectionItem.model';


import { CollectionService } from 'src/app/services/collection.service';
import { ScryfallService } from 'src/app/services/scryfall.service';


@Component({
  selector: 'app-folders-view',
  templateUrl: './folders-view.component.html',
  styleUrls: ['./folders-view.component.scss']
})
export class FoldersViewComponent implements OnInit {

  collectionId: number = -1;
  collectionItemId: number = -1;
  collectionItem?:CollectionItemModel;
  cards:CardModel[] = [];

  constructor(private collectionService: CollectionService, private route: ActivatedRoute, private messageService:MessageService) { 
    this.route.params.subscribe(params => {
      this.collectionId = params['colid'];
      this.collectionItemId = params['id'];
      collectionService.getCollectionItem(this.collectionId,this.collectionItemId).subscribe( (colItem:CollectionItemModel) => {
        this.collectionItem = colItem;
      });

      this.collectionService.getCollectionItemCards(this.collectionId,this.collectionItemId).subscribe( (data:CollectionItemModel[]) => {
        this.cards = data;
      });
    });
  }

  ngOnInit(): void {
  }
  
  saveNotes() {
    this.collectionService.updateCollectionItem(this.collectionId,this.collectionItem!).subscribe( () => {
      this.messageService.add({severity:'success', summary: 'Note saved', detail:'Your notes have been saved'});
    });
  }


}
