import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CardModel } from 'src/app/models/card.model';
import { CollectionModel } from 'src/app/models/collection.model';
import { FolderModel } from 'src/app/models/folder.model';
import { ScryfallCardModel } from 'src/app/models/scryfall-card.model';
import { CardService } from 'src/app/services/card.service';
import { CollectionService } from 'src/app/services/collection.service';
import { ScryfallService } from 'src/app/services/scryfall.service';

@Component({
  selector: 'app-collections-cards',
  templateUrl: './collections-cards.component.html',
  styleUrls: ['./collections-cards.component.scss']
})
export class CollectionsCardsComponent implements OnInit {

  collection?:CollectionModel;
  cards:CardModel[] = [];
  folders:FolderModel[] = [];
  selectedCards:CardModel[] = [];

  showAddModal:boolean = false;
  showEditModal:boolean = false;
  showDeleteModal:boolean = false;

  modalData:CardModel = new CardModel();
  newFolder:FolderModel = new FolderModel();

  
  constructor(private collectionService: CollectionService, private cardService: CardService, private route: ActivatedRoute, private scryfallService:ScryfallService) { 
    this.route.params.subscribe(params => {
      this.collection = collectionService.getCollection(params['colid']);
      this.folders = collectionService.getCollectionFolders(params['colid']);
      this.cards = cardService.getCards(params['id']);
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
