import { Component, OnInit } from '@angular/core';
import { DeckModel } from 'src/app/models/deck.model';
import { DeckService } from 'src/app/services/deck.service';

@Component({
  selector: 'app-decks',
  templateUrl: './decks.component.html',
  styleUrls: ['./decks.component.scss']
})
export class DecksComponent implements OnInit {


  decks:DeckModel[];
  selectedDecks:DeckModel[] = [];

  constructor(private deckService:DeckService) { 
    this.decks = deckService.getDecks();
  }

  ngOnInit(): void {
  }

}
