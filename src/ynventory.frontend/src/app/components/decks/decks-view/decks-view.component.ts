import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';
import { CardModel } from 'src/app/models/card.model';
import { DeckModel } from 'src/app/models/deck.model';
import { DeckService } from 'src/app/services/deck.service';


import { ScryfallService } from 'src/app/services/scryfall.service';


@Component({
  selector: 'app-decks-view',
  templateUrl: './decks-view.component.html',
  styleUrls: ['./decks-view.component.scss']
})
export class DecksViewComponent implements OnInit {

  deckId:number = -1;
  deck?:DeckModel;
  cards:CardModel[] = [];
  selectedCards:CardModel[] = [];
  chartColorDistribution:any;
  chartLevelDistribution:any;

  showAddModal:boolean = false;
  showEditModal:boolean = false;
  showDeleteModal:boolean = false;

  modalData:CardModel = new CardModel();
  modalNewData:CardModel = {};

  chartOptions:any = {
    plugins: {
      legend: {
        position: 'bottom'
      }
    }
  }
  constructor(private deckService:DeckService, private route: ActivatedRoute, private scryfallService:ScryfallService, private messageService:MessageService) { 

    this.route.params.subscribe(params => {
      this.deckId = params['id'];
      this.deckService.getDeck(this.deckId).subscribe( (data:DeckModel) => {
        this.deck = data;
      });
      this.loadData();
    });

  }

  loadData() {
    this.deckService.getDeckCards(this.deckId).subscribe( (data:CardModel[]) => {
      this.cards = data;
    });
  }

  ngOnInit(): void {
  }

  createItem(event:any) {
    this.showAddModal = false;

    if(event){
      this.deckService.createDeckCard(this.deckId, this.modalNewData).subscribe(() => {
        this.loadData();
        this.modalNewData = {};
      });

    }
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


  saveNotes() {
    this.deckService.updateDeck(this.deck!).subscribe( () => {
      this.messageService.add({severity:'success', summary: 'Note saved', detail:'Your notes have been saved'});
    });
  }


}
