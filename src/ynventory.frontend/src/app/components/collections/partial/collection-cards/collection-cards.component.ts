import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CardModel } from 'src/app/models/card.model';
import { CollectionModel } from 'src/app/models/collection.model';
import { CollectionItemModel } from 'src/app/models/collectionItem.model';
import { FolderModel } from 'src/app/models/folder.model';
import { CardService } from 'src/app/services/card.service';
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

  getImageUrl = CardModel.getImageUrl;

  
  constructor(private collectionService: CollectionService, private cardService: CardService, private route: ActivatedRoute, private scryfallService:ScryfallService) { 
    this.route.params.subscribe(params => {
      this.collectionId = params['colid'];

      collectionService.getCollection(this.collectionId).subscribe( (data:CollectionModel) => {
        this.collection = data;
      });

      collectionService.getCollectionItems(this.collectionId).subscribe( (colItems:CollectionItemModel[]) => {
        this.cards = [];

        colItems.forEach( (colItem:CollectionItemModel) => {
          collectionService.getCollectionItemCards(this.collectionId, colItem.id!).subscribe( (colCards:CardModel[]) => {
            this.cards = this.cards.concat(colCards);
          });
        });
      });

       collectionService.getCollectionItems(this.collectionId).subscribe( (colItems:CollectionItemModel[]) => {
        this.collectionItems = colItems;
       });
    });
  }


  ngOnInit(): void {
  }

  createItem(){
    this.showAddModal = false;
  }

  openEditModal(){
    this.modalData = this.selectedCards[0];
    // TODO: Set FOlder name here 
    //  this.newFolder = this.selectedCards[0].folderID;
    this.showEditModal = true;
  }

  saveItem(){
    this.showEditModal = false;
  }

  deleteItem(){
    this.showDeleteModal = false;
  }


}
