import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CardModel } from 'src/app/models/card.model';
import { CollectionModel } from 'src/app/models/collection.model';
import { CollectionItemModel } from 'src/app/models/collectionItem.model';
import { FolderModel } from 'src/app/models/folder.model';

import { CollectionService } from 'src/app/services/collection.service';
import { ScryfallService } from 'src/app/services/scryfall.service';


@Component({
  selector: 'app-collection-cards',
  templateUrl: './collection-cards.component.html',
  styleUrls: ['./collection-cards.component.scss']
})
export class CollectionCardsComponent implements OnInit {

  collectionId:number = -1;
  collection?:CollectionModel;
  cards:CardModel[] = [];
  selectedCards:CardModel[] = [];
  collectionItems:CollectionItemModel[] = [];

  showAddModal:boolean = false;
  showEditModal:boolean = false;
  showDeleteModal:boolean = false;

  modalData:CardModel = new CardModel();
  newFolder:FolderModel = new FolderModel();
  modalDataDelete:CardModel[] = [];

  getImageUrl = CardModel.getImageUrl;

  
  constructor(private collectionService: CollectionService,  private route: ActivatedRoute, private scryfallService:ScryfallService) { 
    this.route.params.subscribe(params => {
      this.collectionId = params['colid'];

      collectionService.getCollection(this.collectionId).subscribe( (data:CollectionModel) => {
        this.collection = data;
      });

      this.loadData();      
    });
  }

  loadData() {
    this.collectionService.getCollectionItems(this.collectionId).subscribe( (colItems:CollectionItemModel[]) => {
      this.cards = [];
      this.selectedCards = [];

      colItems.forEach( (colItem:CollectionItemModel) => {
        this.collectionService.getCollectionItemCards(this.collectionId, colItem.id!).subscribe( (colCards:CardModel[]) => {
          this.cards = this.cards.concat(colCards);
        });
      });
    });

     this.collectionService.getCollectionItems(this.collectionId).subscribe( (colItems:CollectionItemModel[]) => {
      this.collectionItems = colItems;
     });
  }


  ngOnInit(): void {
  }


}
