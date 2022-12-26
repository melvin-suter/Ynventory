import { Component, OnInit, SimpleChange, SimpleChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { CardModel } from 'src/app/models/card.model';
import { FolderModel } from 'src/app/models/folder.model';
import { ScryfallCardModel } from 'src/app/models/scryfall-card.model';
import { CardService } from 'src/app/services/card.service';
import { CollectionService } from 'src/app/services/collection.service';
import { ScryfallService } from 'src/app/services/scryfall.service';

@Component({
  selector: 'app-cards',
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.scss']
})
export class CardsComponent implements OnInit {

  folder?:FolderModel;
  cards:CardModel[] = [];

  searchText:string = "";
  searchResults:ScryfallCardModel[] = [];

  getManaCostList = ScryfallCardModel.getManaCostList;

  createModalOpen:boolean = false;

  constructor(private collectionService: CollectionService, private cardService: CardService, private route: ActivatedRoute, private scryfallService:ScryfallService) { 
    this.route.params.subscribe(params => {
      this.folder = collectionService.getFolder(params['id']);
      this.cards = cardService.getCards(params['id']);
    });
  }

  ngOnInit(): void {
  }

  openAddModal(){
    this.createModalOpen = true;
  }

  searchChanged(){
    this.scryfallService.searchCard(this.searchText).subscribe((data:any) => {
      this.searchResults = data.data;
      console.log(this.searchResults);
    });
  }


}
