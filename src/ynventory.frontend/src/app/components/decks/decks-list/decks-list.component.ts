import { Component, OnInit } from '@angular/core';
import { DeckModel } from 'src/app/models/deck.model';
import { DeckService } from 'src/app/services/deck.service';



@Component({
  selector: 'app-decks-list',
  templateUrl: './decks-list.component.html',
  styleUrls: ['./decks-list.component.scss']
})
export class DecksListComponent implements OnInit {


  decks:DeckModel[] = [];
  selectedDecks:DeckModel[] = [];

  showAddModal:boolean = false;
  showEditModal:boolean = false;
  showDeleteModal:boolean = false;

  modalData:DeckModel = {};

  constructor(private deckService:DeckService) { 
    this.loadData();
  }

  loadData() {
    this.deckService.getDecks().subscribe((data:DeckModel[]) => {
      this.selectedDecks = [];
      this.decks = data;
    });
  }

  ngOnInit(): void {
  }


  createItem(){
    this.deckService.createDeck(this.modalData).subscribe( () => this.loadData());
    this.showAddModal = false;
    this.modalData = {};
  }

  openEditModal(){
    this.modalData = this.selectedDecks[0];
    this.showEditModal = true;
  }

  saveItem(){
    this.deckService.updateDeck(this.modalData).subscribe( () => this.loadData());
    this.showEditModal = false;
    this.modalData = {};
  }

  deleteItem(){
    this.selectedDecks.forEach((deck:DeckModel) => {
      this.deckService.deleteDeck(deck.id!).subscribe( () => {
        this.loadData();
      });
    });
    this.showDeleteModal = false;
    this.modalData = {};
  }
}
