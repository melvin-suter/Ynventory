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
  cards:CardModel[] = [];
  selectedCards:CardModel[] = [];

  showAddModal:boolean = false;
  showEditModal:boolean = false;
  showDeleteModal:boolean = false;

  modalData:CardModel = new CardModel();

  constructor(private collectionService: CollectionService, private cardService: CardService, private route: ActivatedRoute, private scryfallService:ScryfallService) { 
    this.route.params.subscribe(params => {
      this.folder = collectionService.getFolder(params['id']);
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
    this.showEditModal = true;
  }

  saveItem(){
    this.showEditModal = false;
  }

  deleteItem(){
    this.showDeleteModal = false;
  }


}
