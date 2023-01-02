import { Component, OnInit } from '@angular/core';
import { DeckModel } from 'src/app/models/deck.model';



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

  modalData:DeckModel = new DeckModel();

  constructor() { 
    //this.decks = deckService.getDecks();
  }

  ngOnInit(): void {
  }


  createItem(){
    this.showAddModal = false;
  }

  openEditModal(){
    this.modalData = this.selectedDecks[0];
    this.showEditModal = true;
  }

  saveItem(){
    this.showEditModal = false;
  }

  deleteItem(){
    this.showDeleteModal = false;
  }
}
