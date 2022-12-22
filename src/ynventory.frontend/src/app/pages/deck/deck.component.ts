import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CardModel } from 'src/app/models/card.model';
import { DeckModel } from 'src/app/models/deck.model';
import { ScryfallCardModel } from 'src/app/models/scryfall-card.model';
import { CardService } from 'src/app/services/card.service';
import { DeckService } from 'src/app/services/deck.service';
import { ScryfallService } from 'src/app/services/scryfall.service';


@Component({
  selector: 'app-deck',
  templateUrl: './deck.component.html',
  styleUrls: ['./deck.component.scss']
})
export class DeckComponent implements OnInit {

  deck?:DeckModel;
  cards?:CardModel[];
 
  constructor(private deckService: DeckService, private cardService: CardService, private route: ActivatedRoute, private scryfallService:ScryfallService) { 
    this.route.params.subscribe(params => {
      this.deck = deckService.getDeck(params['id']);
      this.cards = cardService.getCards(params['id']);
    });
  }

  ngOnInit(): void {
  }



}
